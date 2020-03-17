@echo off

call node ./Website/wwwroot/scripts/r.js -o ./Website/wwwroot/scripts/bundle-config.js

if ERRORLEVEL 1 (    
	echo ##################################    
    set /p cont= Error occured. Press Enter to exit.
    exit /b -1
)