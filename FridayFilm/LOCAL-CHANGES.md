# FridayFilm: lokal düzəlişlər və frontend

Bu hesabat frontend və backend düzəlişlərini, yoxlamaları və inteqrasiya testləri üçün qalan asılılıqları sənədləşdirir.

## Düzəldilən problemlər

- Register artıq hər istifadəçiyə Admin vermir; User rolu verilir və Identity nəticəsi yoxlanır. İstifadəçi və rol eyni transaction daxilində yaradılır.
- Register access/refresh token vermir. Email təsdiqlənməyən və bloklanmış hesablar token yarada/yeniləyə bilmir. Mövcud access tokenlə sorğu gələndə də hesab yoxlanır.
- Refresh token atomik olaraq istifadə edilmiş sayılır. Paralel sorğular eyni refresh tokeni iki dəfə istifadə edə bilmir.
- Email linki `ApplicationUrls:FrontendBaseUrl` konfiqurasiyasından yaradılır, token URL-encode olunur və console-a yazılmır. Frontend linkdəki tokeni history-dən çıxarır.
- SMTP parametrləri yoxlanır; 465 portunda SSL, digər portlarda STARTTLS işlənir. Email göndərilməsi alınmasa, register hesabın yaradıldığını düzgün bildirir; `POST /api/auth/resend-verification` ilə yenidən göndərmək mümkündür.
- Register/login/resend üçün IP üzrə dəqiqədə 5 sorğu limiti var. Production reverse proxy istifadə olunarsa, etibarlı proxy-lər üzrə ForwardedHeaders konfiqurasiyası ayrıca qurulmalıdır.
- Rol və permission dəyişiklikləri transaction daxilində saxlanır; permission adları canonical formaya çevrilir. Admin/User seed rollarının redaktəsi bloklanır, xüsusi rollar yaradıla və redaktə edilə bilər.
- Permission-lar sorğu vaxtı bazadakı rollardan yoxlanır; köhnə JWT permission-u ilə ləğv edilmiş icazəni istifadə etmək mümkün deyil.
- Public film/kateqoriya/janr/aktyor/rejissor/bio GET endpointləri açıqdır. Yazma əməliyyatları permission policy ilə qorunur.
- CORS icazəli origin siyahısı əlavə olunub. Frontend eyni API hostunda işlədiyi üçün ayrıca proxy və Node server tələb etmir.
- Film detail yazma endpointləri çıxarılıb. Detail Movie create/update daxilində saxlanır. Köhnə GET endpointləri saxlanılıb.
- Movie poster upload: `POST /api/movies/cover`, multipart `file`; maksimum 5 MB JPEG/PNG/WebP. Yalnız movie create/update permission-u ilə işləyir. Cloudinary konfiqurasiyası tələb olunur.
- `GET /api/languages` mövcud database qeydlərini qaytarır. Lang enumu dəyişdirilməyib, yeni dil qeydləri seed edilməyib.
- Film axtarışı, kateqoriya filteri və newest/rating/year/name sıralaması əlavə olunub; filterdən sonra pagination hesablanır.
- Pagination mənfi/sıfır və overflow qiymətlərini rədd edir, maksimum səhifə ölçüsü 100-dür. Siyahı sıralaması stabildir.
- Repository ID axtarışı soft-delete filterini nəzərə alır. Bio üçün çatışmayan soft-delete filteri əlavə olunub.
- Şəkil yenilənəndə mövcud image navigation yüklənir; köhnə remote fayl database save-dən əvvəl silinmir. Köhnə fayllar qəsdən saxlanılır; sonrakı orphan-file cleanup ayrıca işdir.
- Lokal fayl yolunun wwwroot-dan çıxmasına və orijinal fayl adının yola təsirinə icazə verilmir.
- Validation, exception və boş 401/403/404/429 cavabları statusCode/message/path/traceId formatındadır. Validation cavabında errors da var. Gözlənilməyən 500-də daxili exception mətni göstərilmir. Database unique/FK conflict-ləri 409 qaytarır.
- Nullable və migration class-name warning-ləri düzəldilib. Gender default üçün sentinel açıq göstərilib.

## Frontend

`Presentation/FridayFilm.WebApi/wwwroot` daxilində HTML/CSS/JavaScript frontend:

- Ana səhifə və film kataloqu, axtarış, kateqoriya filteri, sıralama, səhifələmə.
- Film detail, aktyor/rejissor siyahıları, xarici treyler linki.
- Login/register, email təsdiqi və məktubun yenidən göndərilməsi.
- Permission-a uyğun film/kateqoriya/janr/aktyor/rejissor/bio/rol idarəetmə formaları.
- Filmə bağlı detail və relation seçimləri, poster URL və upload.
- Mobil və desktop layout, loading/empty/error halları, validation mesajları və silmə təsdiqi.
- Access/refresh tokenlər yalnız yaddaşda saxlanır. Səhifəni tam yeniləyəndə yenidən login tələb olunur. Token refresh paralel sorğularda paylaşılır.
- Streaming/video faylı endpointi backend-də olmadığı üçün film yayımı əlavə edilməyib; treyler açılır.

## İşə salmaq

Əvvəl mövcud PostgreSQL bazasını başladın. Connection string hazırda lokal `localhost:5433` üçündür.
`appsettings.example.json` konfiqurasiya nümunəsidir; real SMTP/JWT/Cloudinary sirlərini User Secrets və ya environment variables ilə verin.

```powershell
cd C:\Desktop\FridayFilm\FridayFilm
dotnet run --project Presentation/FridayFilm.WebApi --launch-profile https
```

Frontend: https://localhost:7243/

Swagger: https://localhost:7243/swagger

HTTP profili ilə işləyirsinizsə, təsdiq linki üçün `ApplicationUrls:FrontendBaseUrl` dəyərini http://localhost:5148 edin.

Yalnız UI və bazasız validation/status yoxlaması üçün:

```powershell
dotnet run --project Presentation/FridayFilm.WebApi --launch-profile http -- --Database:SeedRoles=false
```

Bu parametr bazanı əvəz etmir: login, film siyahısı və CRUD yenə PostgreSQL tələb edir. Default olaraq role seed aktivdir.

## Yoxlamalar və qalan xarici asılılıqlar

```powershell
dotnet test Tests/FridayFilm.Tests/FridayFilm.Tests.csproj
node --test Tests/frontend.test.cjs
```

- 17 backend regression testi və 6 frontend testi keçir.
- HTTP yoxlamaları: frontend 200; yanlış pagination 400; icazəsiz roles GET və movies POST 401; boş register 400; Swagger 200. Poster multipart schema-sı yaradılır.
- Brauzerdə auth ekranları, tələb olunan sahələr və responsive görünüş yoxlanıb.
- Real database CRUD, login/refresh transaction və SMTP/Cloudinary inteqrasiyası başdan sona yoxlanmayıb: PostgreSQL 5433 portunda cavab vermir. Docker log-u `dockerInference` lokal faylına giriş xətası göstərir və engine açılmır. Docker factory reset və ya database silinməsi edilməyib.
- Əvvəl səhvən Admin verilmiş mövcud istifadəçilər avtomatik dəyişdirilməyib: kimlərin həqiqətən Admin qalmalı olduğu ayrıca müəyyən edilməlidir.
- Yeni migration tələb edən schema dəyişikliyi edilməyib. Mövcud database-in migration vəziyyəti bağlantı bərpa olunandan sonra yoxlanmalıdır.
