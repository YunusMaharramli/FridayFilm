@echo off
REM FridayFilm - seed migration. Bu fayla iki defe klikle ise dusur.
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0migrate.ps1"
echo.
pause
