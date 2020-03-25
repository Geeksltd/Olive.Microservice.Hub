@echo off

echo Compiling typescript files ...
call tsc

echo Bundeling ...
call node ./Website/wwwroot/scripts/r.js -o ./Website/wwwroot/scripts/bundle-config.js

echo Replacing content ...
call replace-in-file "./Website/wwwroot/scripts/bundle-built.js" -set "jquery.validate.unobtrusive" "jquery-validate-unobtrusive" -set "jquery-validation" "jquery-validate" -set "../scripts/extensions" "app/extensions" -set "../scripts/model/service" "app/model/service" -set "../scripts/featuresMenu/featuresMenu" "app/featuresMenu/featuresMenu" -set "../scripts/appContent" "app/appContent" -set "../scripts/badgeNumber" "app/badgeNumber" -set "../scripts/toggleCheckbox" "app/toggleCheckbox" -set "../scripts/widgetModule" "app/widgetModule" -set "../scripts/expandCollapse" "app/expandCollapse" -set "../scripts/featuresMenu/breadcrumbMenu" "app/featuresMenu/breadcrumbMenu" -set "../scripts/error/errorTemplates" "app/error/errorTemplates" -set "../scripts/error/errorViewsNavigator" "app/error/errorViewsNavigator" -set "define('hub'" "define('app/hub'" -set "../lib/jquery-ui/ui/version" "jquery-ui/ui/version" -set "../lib/jquery-ui/ui/widget" "jquery-ui/ui/widget" -set "../moment" "moment" -set "../scripts/appPage" "app/appPage" -set "../scripts/overrides/hubAjaxRedirect" "overrides/hubAjaxRedirect" -set "./overrides/hubAjaxRedirect" "overrides/hubAjaxRedirect" -set "../scripts/overrides/hubForm" "overrides/hubForm" -set "./overrides/hubForm" "overrides/hubForm" -set "../scripts/hub" "app/hub" -set "../scripts/overrides/hubUrl" "overrides/hubUrl" -set "./overrides/hubUrl" "overrides/hubUrl" -set "./hubModal" "hubModal" -set "app/hubModal" "hubModal" -set "popper.js" "popper" -set "define(\"jquery-validate-unobtrusive\", [\"jquery\",\"jquery-validate\"], function(){});" "" -set "./hub" "app/hub"

echo Apending content ...
type "append-script.txt" >> "./Website/wwwroot/scripts/bundle-built.js"

echo Minify files ...
call Website\wwwroot\styles\build\SassCompiler.exe Website\compilerconfig.json


if ERRORLEVEL 1 (    
	echo ##################################    
    set /p cont= Error occured. Press Enter to exit.
    exit /b -1
)