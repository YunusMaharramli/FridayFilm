# FridayFilm - seed migration yaradir ve bazaya tetbiq edir.
# Istifade: VS terminalinda ve ya PowerShell-de  ->  .\migrate.ps1

$ErrorActionPreference = 'Stop'
Set-Location -Path $PSScriptRoot

$persistence = 'Infrastructure\FridayFilm.Persistence'
$startup     = 'Presentation\FridayFilm.WebApi'
$migrationName = 'SeedAllData'

Write-Host ''
Write-Host '=== 1/4  dotnet-ef yoxlanilir ===' -ForegroundColor Cyan
$efInstalled = (dotnet tool list --global 2>$null | Select-String -SimpleMatch 'dotnet-ef')
if (-not $efInstalled) {
    Write-Host 'dotnet-ef tapilmadi, qurulur...' -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
} else {
    Write-Host 'dotnet-ef movcuddur.' -ForegroundColor Green
}

Write-Host ''
Write-Host '=== 2/4  Layihe build olunur ===' -ForegroundColor Cyan
dotnet build "$startup" -v minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host 'BUILD UGURSUZ. Yuxaridaki xetani duzeltmeden davam etmek olmaz.' -ForegroundColor Red
    exit 1
}

Write-Host ''
Write-Host "=== 3/4  Migration yaradilir: $migrationName ===" -ForegroundColor Cyan
$existing = Get-ChildItem -Path "$persistence\Migrations" -Filter "*_$migrationName.cs" -ErrorAction SilentlyContinue
if ($existing) {
    Write-Host "'$migrationName' migration-i artiq movcuddur, yenisi yaradilmir." -ForegroundColor Yellow
} else {
    dotnet ef migrations add $migrationName --project "$persistence" --startup-project "$startup"
    if ($LASTEXITCODE -ne 0) {
        Write-Host 'Migration yaradila bilmedi.' -ForegroundColor Red
        exit 1
    }
}

Write-Host ''
Write-Host '=== 4/4  Baza yenilenir ===' -ForegroundColor Cyan
dotnet ef database update --project "$persistence" --startup-project "$startup"
if ($LASTEXITCODE -ne 0) {
    Write-Host ''
    Write-Host 'BAZA YENILENMEDI.' -ForegroundColor Red
    Write-Host 'En cox rast gelinen sebebler:' -ForegroundColor Yellow
    Write-Host '  - PostgreSQL islemir (appsettings.json-da Port=5432 yazilib)'
    Write-Host '  - Kohne seed setirlerinde CreatedDate = DateTimeKind.Unspecified (Npgsql qebul etmir)'
    Write-Host 'Yuxaridaki xeta metnini Claude-a gonder.'
    exit 1
}

Write-Host ''
Write-Host 'HAZIRDIR. Seed data bazaya yazildi.' -ForegroundColor Green
Write-Host 'Indi layiheni ise sala bilersen:  dotnet run --project Presentation\FridayFilm.WebApi --launch-profile https'
Write-Host ''
