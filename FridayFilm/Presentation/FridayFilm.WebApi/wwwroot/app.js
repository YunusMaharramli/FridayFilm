const main = document.querySelector('#main');
const modal = document.querySelector('#modal');
const content = document.querySelector('#modal-content');
const state = { session: null, page: 1, search: '', category: '', sort: 'newest', admin: 'movies', people: 'actors' };
let refreshPromise, renderVersion = 0, toastTimer;
const esc = value => String(value ?? '').replace(/[&<>"']/g, c => ({ '&':'&amp;', '<':'&lt;', '>':'&gt;', '"':'&quot;', "'":'&#39;' }[c]));
const safeUrl = value => { if (typeof value !== 'string' || !value.trim()) return ''; try { const u = new URL(value, location.origin); return ['https:', 'http:'].includes(u.protocol) ? u.href : ''; } catch { return ''; } };
const image = (url, alt, cls = '') => safeUrl(url) ? `<img class="${cls}" src="${esc(safeUrl(url))}" alt="${esc(alt)}" loading="lazy">` : '';
const title = row => row.name || row.fullName || row.description || 'Adsız';
const list = data => Array.isArray(data) ? data : data.data;
const button = (text, attrs = '', cls = '') => `<button class="button ${cls}" ${attrs}>${text}</button>`;
const empty = (heading, message) => `<div class="empty"><h2>${esc(heading)}</h2><p>${esc(message)}</p></div>`;
const loading = () => '<div class="loading" role="status">Yüklənir…</div>';
function toast(message) { const el = document.querySelector('#toast'); el.textContent = message; el.style.display = 'block'; clearTimeout(toastTimer); toastTimer = setTimeout(() => el.style.display = 'none', 4500); }
function claims() { try { return JSON.parse(new TextDecoder().decode(Uint8Array.from(atob(state.session.accessToken.split('.')[1].replace(/-/g,'+').replace(/_/g,'/')), c => c.charCodeAt(0)))); } catch { return {}; } }
function permissions() { const p = claims().permission; return Array.isArray(p) ? p : p ? [p] : []; }
const can = permission => permissions().includes(permission);
const adminResources = () => ['movies','categories','genres','actors','directors','bios','roles'].filter(r => ['create','update','delete'].some(p => can(`${r}.${p}`)));
function setSession(value) { state.session = value; account(); }
async function api(path, { method = 'GET', body, retry = true } = {}) {
  const headers = {};
  if (state.session) headers.Authorization = `Bearer ${state.session.accessToken}`;
  if (body && !(body instanceof FormData)) { headers['Content-Type'] = 'application/json'; body = JSON.stringify(body); }
  let response;
  try { response = await fetch(`/api/${path}`, { method, headers, body, signal: AbortSignal.timeout(15000) }); }
  catch { throw new Error('Serverə qoşulmaq mümkün olmadı. Bağlantını yoxlayıb yenidən sınayın.'); }
  if (response.status === 401 && state.session && retry && !path.startsWith('auth/')) {
    refreshPromise ??= api('auth/refresh', { method:'POST', body:{refreshToken:state.session.refreshToken}, retry:false })
      .then(setSession).catch(error => { setSession(null); throw error; }).finally(() => refreshPromise = null);
    await refreshPromise;
    // The original body may already be encoded; restore it before retrying.
    return api(path, { method, body: typeof body === 'string' ? JSON.parse(body) : body, retry:false });
  }
  const text = await response.text();
  let result; try { result = text ? JSON.parse(text) : null; } catch { result = text; }
  if (!response.ok) {
    const errors = result?.errors ? Object.values(result.errors).flat().join('\n') : '';
    throw new Error(errors || result?.message || (typeof result === 'string' && result.length < 250 ? result : '') || `Sorğu alınmadı (${response.status}).`);
  }
  return result;
}
async function all(resource) {
  const first = await api(`${resource}?size=100&page=1`);
  if (Array.isArray(first)) return first;
  let rows = [...first.data];
  for (let page = 2; page <= first.totalPages; page++) rows.push(...(await api(`${resource}?size=100&page=${page}`)).data);
  return rows;
}
function account() {
  document.querySelector('#account').innerHTML = state.session
    ? `${adminResources().length ? '<a href="#admin" class="button small">İdarəetmə</a>' : ''}${button('Çıxış', 'data-action="logout"', 'ghost small')}`
    : `<a href="#login" class="subtle-link">Daxil ol</a><a href="#register" class="button small">Qeydiyyat ↗</a>`;
}
function card(movie) {
  return `<a class="movie-card" href="#movie/${esc(movie.id)}"><div class="poster">${image(movie.coverImg, movie.name)}<span class="rating">★ ${Number(movie.imdb).toFixed(1)}</span></div><h3>${esc(movie.name)}</h3><div class="metadata"><span>${movie.year}</span><span>${esc(duration(movie.duration))}</span></div></a>`;
}
function duration(value) { const parts = (value || '').split(':'); return parts.length >= 2 ? `${Number(parts[0]) * 60 + Number(parts[1])} dəq` : ''; }
async function catalog(home, version) {
  const params = new URLSearchParams({ page:state.page, size:home ? 5 : 20, search:state.search, sort:state.sort });
  if (state.category) params.set('categoryId', state.category);
  const [movies, categories] = await Promise.all([api(`movies?${params}`), all('categories')]);
  if (version !== renderVersion) return;
  const featured = home ? movies.data[0] : null;
  main.innerHTML = `<div class="page">${home ? `<section class="hero">${featured ? image(featured.coverImg, '', 'hero-backdrop') : '<div class="hero-art" aria-hidden="true">f.</div>'}<div class="hero-content"><p class="eyebrow">FRIDAY PICKS · KİNOYA BİR AZ DAHA YAXIN</p><h1>${featured ? esc(featured.name) : 'Yaxşı hekayələr<br>burada başlayır.'}</h1>${featured ? `<div class="metadata"><span class="rating">★ ${Number(featured.imdb).toFixed(1)} IMDb</span><span>${featured.year}</span><span>${esc(duration(featured.duration))}</span></div>` : ''}<p class="description">${featured ? esc((featured.movieDetail?.description || '').slice(0,220)) : 'Yeni dünyalar, unudulmaz personajlar. Növbəti sevimli filmini FridayFilm ilə kəşf et.'}</p><a class="button primary" href="${featured ? `#movie/${esc(featured.id)}` : '#catalog'}">${featured ? 'Filmi kəşf et' : 'Filmlərə bax'} <span>↗</span></a></div></section>` : ''}<div class="section-head"><div><p class="eyebrow">SƏNİN KİNO DÜNYAN</p><h${home ? '2' : '1'}>${home ? 'Növbəti film gecən üçün' : 'Film kataloqu'}</h${home ? '2' : '1'}><p>${movies.totalCount} film · Hər hekayə yeni bir dünyadır</p></div>${home ? '<a class="subtle-link" href="#catalog">Bütün filmlər ↗</a>' : ''}</div>${!home ? `<form id="search-form" class="toolbar"><label class="visually-hidden" for="search">Film axtar</label><input class="search" id="search" name="search" placeholder="Film adı ilə axtar…" value="${esc(state.search)}" maxlength="250"><label class="visually-hidden" for="sort">Sıralama</label><select id="sort" name="sort">${[['newest','Yeni əlavə olunanlar'],['rating','IMDb reytinqi'],['year','Buraxılış ili'],['name','Ad: A–Z']].map(([v,t]) => `<option value="${v}" ${state.sort===v?'selected':''}>${t}</option>`).join('')}</select>${button('Axtar','','primary')}</form><div class="chips">${button('Hamısı','data-category=""',`chip ${!state.category?'selected':''}`)}${categories.map(c => `<button class="chip ${state.category===c.id?'selected':''}" data-category="${esc(c.id)}">${esc(c.name)}</button>`).join('')}</div>` : ''}<div class="grid">${movies.data.length ? movies.data.map(card).join('') : empty('Hələ film yoxdur', state.search || state.category ? 'Axtarış və ya kateqoriyanı dəyişərək yenidən sınayın.' : 'Kataloqa əlavə olunan filmlər burada görünəcək.')}</div>${!home && movies.totalPages > 1 ? `<div class="pagination">${button('← Əvvəlki',`data-page="${state.page-1}" ${state.page===1?'disabled':''}`)}<span>${state.page} / ${movies.totalPages}</span>${button('Növbəti →',`data-page="${state.page+1}" ${state.page>=movies.totalPages?'disabled':''}`)}</div>` : ''}</div>`;
}
async function movieDetail(id, version) {
  const [movie, genres, actors, directors, categories, languages] = await Promise.all([api(`movies/${encodeURIComponent(id)}`), all('genres'), all('actors'), all('directors'), all('categories'), all('languages')]);
  if (version !== renderVersion) return;
  const names = (rows, ids) => rows.filter(x => ids.includes(x.id)).map(title).join(', ') || 'Göstərilməyib';
  const trailer = safeUrl(movie.movieDetail?.trailerUrl);
  main.innerHTML = `<div class="page"><a class="subtle-link" href="#catalog">← Kataloqa qayıt</a><section class="detail-layout"><div class="poster">${image(movie.coverImg,movie.name)}</div><div><p class="eyebrow">FRIDAYFILM · FİLM DETALLARI</p><h1>${esc(movie.name)}</h1><div class="metadata"><span class="rating">★ ${movie.imdb} IMDb</span><span>${movie.year}</span><span>${esc(duration(movie.duration))}</span><span>${esc(languages.find(x=>x.id===movie.languageId)?.name || '')}</span></div><div class="detail-tags">${genres.filter(g=>movie.genreIds.includes(g.id)).map(g=>`<span>${esc(g.name)}</span>`).join('')}</div><p>${esc(movie.movieDetail?.description || 'Bu film üçün açıqlama əlavə edilməyib.')}</p><p><strong>Rejissor:</strong> ${esc(names(directors,movie.directorIds))}<br><strong>Aktyorlar:</strong> ${esc(names(actors,movie.actorIds))}<br><strong>Kateqoriya:</strong> ${esc(categories.find(x=>x.id===movie.categoryId)?.name || 'Göstərilməyib')}</p><div class="form-actions">${trailer ? `<a class="button primary" href="${esc(trailer)}" target="_blank" rel="noopener noreferrer">▷ Treylerə bax</a>` : ''}${can('movies.update') ? button('Redaktə et',`data-edit-movie="${esc(movie.id)}"`) : ''}</div></div></section></div>`;
}
const field = (name,label,value='',type='text',attrs='') => `<div><label for="f-${name}">${label}</label><input id="f-${name}" name="${name}" type="${type}" value="${esc(value)}" ${attrs}></div>`;
const area = (name,label,value='',attrs='') => `<div class="full"><label for="f-${name}">${label}</label><textarea id="f-${name}" name="${name}" ${attrs}>${esc(value)}</textarea></div>`;
function authPage(mode) {
  const register = mode === 'register', resend = mode === 'resend';
  main.innerHTML = `<div class="page"><section class="auth-page"><p class="eyebrow">FRIDAYFILM HESABIN</p><h1>${register?'Kino dünyana qoşul':resend?'Təsdiq məktubu':'Yenidən xoş gəldin'}</h1><p class="notice">${register?'Qeydiyyatdan sonra emailinə göndərilən linklə hesabını təsdiqlə.':resend?'Məktub gəlməyibsə, email ünvanını yaz. Spam qovluğunu da yoxla.':'Sevimli hekayələrinə bir addım qalıb.'}</p><form id="auth-form" data-mode="${mode}">${register?field('fullName','Ad və soyad','','text','required maxlength="100" autocomplete="name"'):''}${field('email','Email','','email','required maxlength="256" autocomplete="email"')}${!resend?field('password','Şifrə','','password',`required maxlength="100" ${register?'minlength="8" autocomplete="new-password"':'autocomplete="current-password"'}`):''}${register?'<p class="notice">Şifrə ən azı 8 simvol, böyük və kiçik hərf, rəqəm və xüsusi simvoldan ibarət olmalıdır.</p>':''}<p class="form-error" role="alert"></p><p class="form-success" role="status"></p><div class="form-actions">${button(register?'Qeydiyyatdan keç':resend?'Yenidən göndər':'Daxil ol','','primary')}</div></form><div class="form-actions"><a class="subtle-link" href="${register?'#login':'#register'}">${register?'Hesabım var':'Hesab yarat'}</a><a class="subtle-link" href="#resend">Emaili təsdiqlə</a></div></section></div>`;
}
async function verifyPage() {
  const query = new URLSearchParams(location.search);
  const userId = query.get('userId'), token = query.get('token');
  // Remove sensitive verification parameters from browser history immediately.
  history.replaceState(null,'',`${location.pathname}#verify-email`);
  main.innerHTML = `<div class="page"><section class="auth-page"><p class="eyebrow">EMAIL TƏSDİQİ</p><h1>Hesabını təsdiqlə</h1><p class="notice">Email ünvanını təsdiqləmək üçün aşağıdakı düyməni bas.</p><p class="form-error" role="alert"></p>${button('Emailimi təsdiqlə','id="verify-button"','primary')}</section></div>`;
  document.querySelector('#verify-button').onclick = async event => {
    event.target.disabled = true;
    try {
      if (!userId || !token) throw new Error('Link məlumatları çatışmır. Emaildəki linki yenidən açın və ya yeni məktub istəyin.');
      await api(`auth/verify-email?${new URLSearchParams({userId,token})}`);
      main.innerHTML = `<div class="page">${empty('Email təsdiqləndi','İndi hesabınıza daxil ola bilərsiniz.')}<div class="form-actions"><a class="button primary" href="#login">Daxil ol</a></div></div>`;
    } catch(error) { document.querySelector('.form-error').textContent = error.message; event.target.disabled = false; }
  };
}
async function people(version) {
  const rows = await all(state.people);
  if(version !== renderVersion) return;
  main.innerHTML = `<div class="page"><div class="section-head"><div><p class="eyebrow">KADRIN ÖNÜNDƏ VƏ ARXASINDA</p><h1>Kino insanları</h1></div></div><div class="chips"><button class="chip ${state.people==='actors'?'selected':''}" data-people="actors">Aktyorlar</button><button class="chip ${state.people==='directors'?'selected':''}" data-people="directors">Rejissorlar</button></div><div class="people">${rows.length ? rows.map(p=>`<article class="person"><div class="initials">${esc(p.fullName.split(' ').map(x=>x[0]).slice(0,2).join(''))}</div><h3>${esc(p.fullName)}</h3><span class="eyebrow">${esc(p.nationality)}</span><p>${esc(p.bio || 'Bioqrafiya hələ əlavə edilməyib.')}</p></article>`).join('') : empty('Hələ məlumat yoxdur','Yeni kino insanları burada görünəcək.')}</div></div>`;
}
const resourceLabels = {movies:'Filmlər',categories:'Kateqoriyalar',genres:'Janrlar',actors:'Aktyorlar',directors:'Rejissorlar',bios:'Əlaqə məlumatları',roles:'Rollar'};
async function admin(version) {
  const resources = adminResources();
  if(!resources.length) { main.innerHTML = `<div class="page">${empty('İcazə tələb olunur','İdarəetmə icazəsi olan hesabla daxil olun.')}<a href="#login" class="button">Daxil ol</a></div>`; return; }
  if(!resources.includes(state.admin)) state.admin=resources[0];
  const resource = state.admin;
  const rows = resource === 'roles' && !can('roles.read') ? [] : await all(resource);
  if(version !== renderVersion) return;
  main.innerHTML = `<div class="page"><div class="section-head"><div><p class="eyebrow">FRIDAYFILM STUDIO</p><h1>Kataloqu idarə et</h1><p>Dəyişikliklər birbaşa film kataloqunda görünür.</p></div></div><div class="admin-layout"><aside class="admin-nav">${resources.map(r=>button(resourceLabels[r],`data-resource="${r}"`,r===resource?'primary':'ghost')).join('')}</aside><section><div class="section-head" style="margin-top:0"><h2>${resourceLabels[resource]}</h2>${can(`${resource}.create`)?button('+ Əlavə et','data-action="create"','primary'):''}</div><div class="table-wrap"><table><thead><tr><th>AD</th><th>MƏLUMAT</th><th>ƏMƏLİYYAT</th></tr></thead><tbody>${rows.map(r=>`<tr><td>${esc(title(r).slice(0,85))}</td><td>${esc(r.year || r.nationality || (r.permissions ? `${r.permissions.length} icazə` : ''))}</td><td>${can(`${resource}.update`)?button('Redaktə',`data-edit="${esc(r.id)}"`,'small'):''} ${can(`${resource}.delete`)?button('Sil',`data-delete="${esc(r.id)}"`,'small danger'):''}</td></tr>`).join('')}</tbody></table>${rows.length?'':empty('Siyahı boşdur','Yeni məlumat əlavə edərək başla.')}</div></section></div></div>`;
}
function select(name,label,rows,selected=[],multiple=false) {
  return `<div><label for="f-${name}">${label}</label><select id="f-${name}" name="${name}" ${multiple?'multiple':'required'}>${multiple?'':'<option value="">Seçin</option>'}${rows.map(x=>`<option value="${esc(x.id)}" ${selected.includes(x.id)?'selected':''}>${esc(title(x))}</option>`).join('')}</select>${multiple?'<small>Bir neçə seçim üçün Ctrl / Cmd istifadə et.</small>':''}</div>`;
}
async function editor(resource,id) {
  content.innerHTML=loading(); modal.showModal();
  try {
    const row=id ? resource==='roles' ? (await all('roles')).find(x=>x.id===id) : await api(`${resource}/${encodeURIComponent(id)}`) : {};
    let fields='';
    if(resource==='movies') {
      const [categories,languages,genres,actors,directors]=await Promise.all(['categories','languages','genres','actors','directors'].map(all));
      fields=field('name','Filmin adı',row.name,'text','required maxlength="250"')+field('year','Buraxılış ili',row.year || new Date().getFullYear(),'number','required min="1" max="9999"')+field('imdb','IMDb',row.imdb ?? 0,'number','required min="0" max="10" step="0.1"')+field('minutes','Müddət (dəqiqə)',row.duration?Number(row.duration.split(':')[0])*60+Number(row.duration.split(':')[1]):90,'number','required min="1" max="10000"')+field('coverImg','Poster URL',row.coverImg,'url','required maxlength="500"')+field('coverFile','Və ya poster yüklə','','file','accept="image/png,image/jpeg,image/webp"')+select('categoryId','Kateqoriya',categories,[row.categoryId])+select('languageId','Dil',languages,[row.languageId])+select('genreIds','Janrlar',genres,row.genreIds || [],true)+select('actorIds','Aktyorlar',actors,row.actorIds || [],true)+select('directorIds','Rejissorlar',directors,row.directorIds || [],true)+area('description','Film haqqında',row.movieDetail?.description,'required maxlength="2000"')+field('trailerUrl','Treyler URL',row.movieDetail?.trailerUrl,'url','required maxlength="500"');
      if(!languages.length) fields+='<p class="notice full">Bazaya dil əlavə edilməyib. Film yaratmaq üçün mövcud dil qeydi lazımdır.</p>';
    } else if(['categories','genres'].includes(resource)) fields=field('name','Ad',row.name,'text','required maxlength="100"');
    else if(['actors','directors'].includes(resource)) fields=field('fullName','Ad və soyad',row.fullName,'text','required maxlength="150"')+field('nationality','Milliyyət',row.nationality,'text','required maxlength="100"')+select('gender','Cinsiyyət',[{id:'Male',name:'Kişi'},{id:'Female',name:'Qadın'},{id:'Other',name:'Digər'}],[row.gender || 'Other'])+(resource==='actors'?field('nickname','Ləqəb',row.nickname):'')+area('bio','Bioqrafiya',row.bio,'maxlength="2000"')+field('photo','Şəkil','','file','accept="image/png,image/jpeg,image/webp"');
    else if(resource==='bios') fields=area('description','Açıqlama',row.description)+field('contactEmail','Əlaqə emaili',row.contactEmail,'email')+field('contactPhone','Telefon',row.contactPhone,'tel')+field('instagramUrl','Instagram',row.instagramUrl,'url')+field('facebookUrl','Facebook',row.facebookUrl,'url')+field('twitterUrl','Twitter',row.twitterUrl,'url')+field('logoPhoto','Loqo','','file','accept="image/png,image/jpeg,image/webp"');
    else if(resource==='roles') { const allPermissions=await api('roles/permissions'); fields=field('name','Rolun adı',row.name,'text','required maxlength="100"')+`<div class="full"><label>İcazələr</label><div class="permission-list">${allPermissions.map(p=>`<label><input type="checkbox" name="permissions" value="${esc(p)}" ${row.permissions?.includes(p)?'checked':''}>${esc(p)}</label>`).join('')}</div></div>`; }
    content.innerHTML=`<p class="eyebrow">${resourceLabels[resource]}</p><h2 id="modal-title">${id?'Məlumatı yenilə':'Yeni əlavə et'}</h2><form id="editor-form" data-resource="${resource}" data-id="${esc(id || '')}"><div class="form-grid">${fields}</div><p class="form-error" role="alert"></p><div class="form-actions">${button('Yadda saxla','','primary')}${button('Ləğv et','type="button" data-action="close"','ghost')}</div></form>`;
  } catch(error) { content.innerHTML=`<h2 id="modal-title">Məlumat yüklənmədi</h2><p class="form-error">${esc(error.message)}</p>`; }
}
async function submitForm(form,task) {
  const submit=form.querySelector('button:not([type="button"])'); submit.disabled=true;
  form.querySelector('.form-error').textContent='';
  try { await task(); } catch(error) { form.querySelector('.form-error').textContent=error.message; } finally { submit.disabled=false; }
}
document.addEventListener('submit',event=>{
  const form=event.target; event.preventDefault();
  if(form.id==='search-form'){state.search=form.search.value.trim();state.sort=form.sort.value;state.page=1;render();}
  if(form.id==='auth-form') submitForm(form,async()=>{
    const data=Object.fromEntries(new FormData(form)); const mode=form.dataset.mode;
    const result=await api(`auth/${mode==='resend'?'resend-verification':mode}`,{method:'POST',body:data});
    if(mode==='login'){setSession(result);location.hash='home';toast('Xoş gəldin!');}
    else {form.querySelector('.form-success').textContent=result.message;form.reset();}
  });
  if(form.id==='editor-form') submitForm(form,async()=>{
    const data=new FormData(form), resource=form.dataset.resource, id=form.dataset.id;
    let body;
    if(resource==='movies') {
      const file=data.get('coverFile'); let coverImg=data.get('coverImg');
      if(file?.size){const upload=new FormData();upload.append('file',file);coverImg=(await api('movies/cover',{method:'POST',body:upload})).url;}
      const minutes=Number(data.get('minutes'));
      body={name:data.get('name'),year:Number(data.get('year')),imdb:Number(data.get('imdb')),duration:`${String(Math.floor(minutes/60)).padStart(2,'0')}:${String(minutes%60).padStart(2,'0')}:00`,coverImg,categoryId:data.get('categoryId'),languageId:data.get('languageId'),genreIds:data.getAll('genreIds'),actorIds:data.getAll('actorIds'),directorIds:data.getAll('directorIds'),movieDetail:{description:data.get('description'),trailerUrl:data.get('trailerUrl')}};
    } else if(['actors','directors','bios'].includes(resource)){body=data; for(const [k,v] of [...body]) if(v instanceof File && !v.size) body.delete(k);}
    else {body=Object.fromEntries(data);if(resource==='roles') body.permissions=data.getAll('permissions');}
    await api(`${resource}${id?`/${encodeURIComponent(id)}`:''}`,{method:id?'PUT':'POST',body});
    modal.close();toast('Dəyişiklik yadda saxlanıldı.');await render();
  });
});
document.addEventListener('change',event=>{
  if(event.target.name==='coverFile') document.querySelector('[name="coverImg"]').required=!event.target.files.length;
});
document.addEventListener('click',async event=>{
  const target=event.target.closest('button,[data-edit-movie]'); if(!target) return;
  try {
    if(target.dataset.action==='logout'){const token=state.session?.refreshToken;try{if(token)await api('auth/logout',{method:'POST',body:{refreshToken:token},retry:false});}finally{setSession(null);location.hash='home';await render();}}
    if(target.dataset.action==='close') modal.close();
    if(target.dataset.action==='retry') render();
    if(target.dataset.category!==undefined){state.category=target.dataset.category;state.page=1;render();}
    if(target.dataset.page){state.page=Number(target.dataset.page);render();}
    if(target.dataset.people){state.people=target.dataset.people;render();}
    if(target.dataset.resource){state.admin=target.dataset.resource;render();}
    if(target.dataset.action==='create') editor(state.admin);
    if(target.dataset.edit) editor(state.admin,target.dataset.edit);
    if(target.dataset.editMovie) editor('movies',target.dataset.editMovie);
    if(target.dataset.delete){
      const resource=state.admin,id=target.dataset.delete;
      content.innerHTML=`<h2 id="modal-title">Silmək istəyirsən?</h2><p>Bu məlumat kataloqdan çıxarılacaq.</p><div class="form-actions">${button('Bəli, sil','id="confirm-delete"','danger')}${button('Ləğv et','data-action="close"')}</div><p class="form-error" role="alert"></p>`;modal.showModal();
      document.querySelector('#confirm-delete').onclick=async e=>{e.target.disabled=true;try{await api(`${resource}/${encodeURIComponent(id)}`,{method:'DELETE'});modal.close();toast('Məlumat silindi.');render();}catch(error){content.querySelector('.form-error').textContent=error.message;e.target.disabled=false;}};
    }
  }catch(error){toast(error.message);}
});
modal.querySelector('.close').onclick=()=>modal.close();
document.addEventListener('error',event=>{if(event.target instanceof HTMLImageElement) event.target.remove();},true);
async function render() {
  const version=++renderVersion;
  const [page,id]=location.hash.slice(1).split('/');
  document.querySelectorAll('[data-nav]').forEach(el=>el.classList.toggle('active',el.dataset.nav===(page || 'home')));
  account();main.innerHTML=loading();
  try {
    if(['login','register','resend'].includes(page)) authPage(page);
    else if(page==='verify-email') await verifyPage();
    else if(page==='movie' && id) await movieDetail(id,version);
    else if(page==='people') await people(version);
    else if(page==='admin') await admin(version);
    else await catalog(page!=='catalog',version);
  }catch(error){if(version===renderVersion)main.innerHTML=`<div class="page">${!page || page==='home' ? '<section class="hero"><div class="hero-art" aria-hidden="true">f.</div><div class="hero-content"><p class="eyebrow">FRIDAY PICKS · KİNOYA BİR AZ DAHA YAXIN</p><h1>Yaxşı hekayələr<br>burada başlayır.</h1><p class="description">Yeni dünyalar, unudulmaz personajlar. Növbəti sevimli filmini FridayFilm ilə kəşf et.</p><a href="#catalog" class="button primary">Filmlərə bax ↗</a></div></section><div class="section-head"><h2>Film kataloqu</h2></div>' : ''}${empty('Məlumatı yükləmək alınmadı',error.message)}<div class="form-actions">${button('Yenidən sına','data-action="retry"','primary')}<a href="#login" class="button">Daxil ol</a></div></div>`;}
}
window.addEventListener('hashchange',()=>{state.page=1;render();window.scrollTo(0,0);});
document.querySelector('#year').textContent=new Date().getFullYear();
render();
