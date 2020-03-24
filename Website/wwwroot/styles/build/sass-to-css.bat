@echo off

ECHO.
ECHO ::::::::: Rebuilding sass files :::::::::::::::::::::::::::::::::
ECHO.
call Website\wwwroot\styles\build\SassCompiler.exe ..\..\..\Compilerconfig.json 
