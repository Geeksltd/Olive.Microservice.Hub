@echo off

echo Compiling typescript
call tsc

echo Bundeling
call node ./Website/wwwroot/scripts/r.js -o ./Website/wwwroot/scripts/bundle-config.js

echo Replacing content
call replace-in-file "bundle-built.js" -set "jquery.validate.unobtrusive" "jquery-validate-unobtrusive" -set "jquery-validation" "jquery-validate" -set "../scripts/extensions" "app/extensions" -set "../scripts/model/service" "app/model/service" -set "../scripts/featuresMenu/featuresMenu" "app/featuresMenu/featuresMenu" -set "../scripts/appContent" "app/appContent" -set "../scripts/badgeNumber" "app/badgeNumber" -set "../scripts/toggleCheckbox" "app/toggleCheckbox" -set "../scripts/widgetModule" "app/widgetModule" -set "../scripts/expandCollapse" "app/expandCollapse" -set "../scripts/featuresMenu/breadcrumbMenu" "app/featuresMenu/breadcrumbMenu" -set "../scripts/error/errorTemplates" "app/error/errorTemplates" -set "../scripts/error/errorViewsNavigator" "app/error/errorViewsNavigator" -set "define('hub'" "define('app/hub'" -set "../lib/jquery-ui/ui/version" "jquery-ui/ui/version" -set "../lib/jquery-ui/ui/widget" "jquery-ui/ui/widget" -set "../moment" "moment" -set "appPage.js" "app/appPage" -set "./overrides/hubAjaxRedirect" "overrides/hubAjaxRedirect" -set "./overrides/hubForm" "overrides/hubForm" -set "./hubServices" "hubServices" -set "./hub" "app/hub" -set "./overrides/hubUrl" "overrides/hubUrl" -set "./hubModal" "hubModal" -set "app/hubModal" "hubModal" -set "popper.js" "popper"

echo Minify files
call ./Website/wwwroot/styles/sass-to-css.bat    

if ERRORLEVEL 1 (    
	echo ##################################    
    set /p cont= Error occured. Press Enter to exit.
    exit /b -1
)