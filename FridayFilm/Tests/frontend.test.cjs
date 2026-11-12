const {test} = require('node:test');
const assert = require('node:assert/strict');
const vm = require('node:vm');
const fs = require('node:fs');
const path = require('node:path');
const source = fs.readFileSync(path.join(__dirname,'../Presentation/FridayFilm.WebApi/wwwroot/app.js'),'utf8');
function harness(fetch) {
  const elements = new Map();
  const element = selector => {
    if(!elements.has(selector)) elements.set(selector,{innerHTML:'',textContent:'',style:{},querySelector:element});
    return elements.get(selector);
  };
  const sandbox = {URL,URLSearchParams,TextDecoder,Uint8Array,FormData,File,AbortSignal,atob,setTimeout,clearTimeout,fetch,
    location:{hash:'#login',origin:'http://localhost:5148'},document:{querySelector:element,querySelectorAll:()=>[],addEventListener:()=>{}},
    window:{addEventListener:()=>{}},history:{replaceState:()=>{}}};
  vm.createContext(sandbox);
  vm.runInContext(source+'\nglobalThis.subject={api,state,setSession,safeUrl,esc};',sandbox);
  return sandbox.subject;
}
test('unsafe and missing image/trailer URLs are rejected',()=>{
  const s=harness();
  for(const input of [null,undefined,'',' ','javascript:alert(1)','data:text/html,hello']) assert.equal(s.safeUrl(input),'');
  assert.equal(s.safeUrl('https://example.com/poster.jpg'),'https://example.com/poster.jpg');
});
test('API content is HTML escaped',()=>{
  const s=harness();
  assert.equal(s.esc('<img onerror="alert(1)">'),'&lt;img onerror=&quot;alert(1)&quot;&gt;');
});
test('validation messages reach the form',async()=>{
  const s=harness(async()=>new Response(JSON.stringify({errors:{Name:['Ad tələb olunur.']}}),{status:400}));
  await assert.rejects(s.api('movies'),/Ad tələb olunur/);
});
test('concurrent unauthorized requests share one refresh and retry their requests',async()=>{
  let refreshes=0;
  const s=harness(async(url,options)=>{
    if(url==='/api/auth/refresh') {refreshes++;await new Promise(resolve=>setTimeout(resolve,20));return new Response(JSON.stringify({accessToken:'new',refreshToken:'next'}));}
    return options.headers.Authorization==='Bearer new' ? new Response(JSON.stringify({ok:true})) : new Response('{}',{status:401});
  });
  s.setSession({accessToken:'old',refreshToken:'initial'});
  const results=await Promise.all([s.api('movies'),s.api('roles')]);
  assert.equal(refreshes,1);assert.ok(results.every(x=>x.ok));
});
test('failed refresh clears the session',async()=>{
  const s=harness(async()=>new Response('{}',{status:401}));
  s.setSession({accessToken:'old',refreshToken:'expired'});
  await assert.rejects(s.api('roles'));
  assert.equal(s.state.session,null);
});
test('network failures have a readable message',async()=>{
  const s=harness(async()=>{throw new Error('ECONNREFUSED');});
  await assert.rejects(s.api('movies'),/Serverə qoşulmaq mümkün olmadı/);
});
