using System.Threading.Tasks;
using Domain;
using Olive.Mvc;


namespace Olive.Hub
{
    public abstract class DefaultContainerLayout<TViewModel> : BasePage<TViewModel> where TViewModel : IViewModel
    {
        protected DefaultContainerLayout(TViewModel model) : base(model)
        {
            TimeOut = Config.Get("Authentication: Timeout", defaultValue: 20).Minutes().TotalSeconds + 10;
        }

        public double TimeOut { get; set; } = Config.Get("Authentication: Timeout", defaultValue: 20).Minutes().TotalSeconds + 10;

        public string LeftMenu { get; set; }

        public override async Task<string> Render()
        {
            LeftMenu = ViewData["LeftMenu"].ToStringOrEmpty();

            var result = $@"
                            <!DOCTYPE html>
                            <html class=""hub-service"">
                            <head>
                                <meta charset=""utf-8"" />
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <meta http-equiv=""refresh"" content=""{TimeOut}"">
                                <title>{ViewData["Title"]}</title>
                                <link rel='stylesheet' href=""styles/theme.min.css?v=%APP_VERSION%"" type='text/css' />
                                <link rel='stylesheet' href=""styles/hub/hub.min.css?v=%APP_VERSION%"" type='text/css' />
                                <link rel=""icon"" media=""all"" type=""image/x-icon"" href=""img/favicon.png"" />
                                <link rel=""shortcut icon"" href=""img/favicon.ico"">

                                <meta name=""apple-mobile-web-app-capable"" content=""yes"">
                                <meta name=""apple-mobile-web-app-status-bar-style"" content=""#42AAA9"">
                                <meta name=""apple-mobile-web-app-title"" content=""Geeks Hub Application"">

                                <link rel=""mask-icon"" href=""/images/fav/safari-pinned-tab.svg"" color=""#5bbad5"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 414px) and (device-height: 896px) and (-webkit-device-pixel-ratio: 3)"" href=""/img/pwa/splash/iphonexsmax_splash.png"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 414px) and (device-height: 896px) and (-webkit-device-pixel-ratio: 2)"" href=""/img/pwa/splash/iphonexr_splash.png"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 375px) and (device-height: 812px) and (-webkit-device-pixel-ratio: 3)"" href=""/img/pwa/splash/iphonex_splash.png"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 375px) and (device-height: 667px) and (-webkit-device-pixel-ratio: 2)"" href=""/img/pwa/splash/iphone6_splash.png"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 414px) and (device-height: 736px) and (-webkit-device-pixel-ratio: 3)"" href=""/img/pwa/splash/iphoneplus_splash.png"" style=""user-select: text;"">
                                <link rel=""apple-touch-startup-image"" media=""(device-width: 320px) and (device-height: 568px) and (-webkit-device-pixel-ratio: 2)"" href=""/img/pwa/splash/iphone5_splash.png"" style=""user-select: text;"">

                                <link rel=""apple-touch-icon"" sizes=""57x57"" href=""img/pwa/icon57.png"">
                                <link rel=""apple-touch-icon"" sizes=""76x76"" href=""img/pwa/icon76.png"">
                                <link rel=""apple-touch-icon"" sizes=""114x114"" href=""img/pwa/icon114.png"">
                                <link rel=""apple-touch-icon"" sizes=""167x167"" href=""img/pwa/icon167.png"">
                                <link rel=""apple-touch-icon"" sizes=""144x144"" href=""img/pwa/icon144.png"">
                                <link rel=""apple-touch-icon"" sizes=""152x152"" href=""img/pwa/icon152.png"">
                                <link rel=""apple-touch-icon"" sizes=""180x180"" href=""img/pwa/icon180.png"">
                                <link rel=""apple-touch-icon"" sizes=""192x192"" href=""img/pwa/icon192.png"">

                                <meta name=""msapplication-TileImage"" content=""img/pwa/icon144.png"">
                                <meta name=""msapplication-TileColor"" content=""green"">
                                <meta name=""msapplication-starturl"" content=""/"">

                                <!-- Manifest.json-->
                                <link rel=""manifest"" href=""manifest.json"">
                                <meta name=""mobile-web-app-capable"" content=""yes"">
                                <meta name=""theme-color"" content=""#42AAA9"">

                                <script type=""text/javascript"">
                                    window[""services""] = {Service.ToJson()?.Raw()}
                                </script>
                                <script src=""lib/requirejs/require.js"" data-main=""/scripts/references.js?v=%APP_VERSION%""></script>
                            </head>
                            <body>
                                <div class=""container-fluid p-0"">
                                    <div class=""page row m-0"">
                                        {await GenerateLeftMenu()}           
                                        <div class=""content"">                
                                            {await GenerateTopmenuWrapper()}
                                            <div class=""content-body"">
                                                { await GenerateUserInfo()}                    
                                                <service of=""hub"">
                                                    {await RenderBodyAjax()}
                                                    <div class=""feature-frame-view view-body"" id=""iFrameHolder"">
                                                        <iframe class='view-frame embed-responsive-item w-100 h-100' name='view-frame'></iframe>
                                                    </div>
                                                </service>
                                            </div>
                                        </div>            
                                        {GenerateLeftDownMenu()}
                                    </div>
                                </div>
                                {Html.DevCommandsWidget()}
                            </body>
                            </html>";

            return result;
        }

        private async Task<string> GenerateTopmenuWrapper()
        {
            var topMenuViewModel = new ViewModel.FeaturesTopMenuWrapper
            {
                Markup = (await AuthroziedFeatureInfo.RenderMenuJson()).ToString(),
                IsVisible = User.Identity.IsAuthenticated
            };

            return await new FeaturesTopMenuWrapperView(topMenuViewModel).Render();
        }

        private async Task<string> GenerateUserInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                return $@"<div class=""w-100 d-flex d-lg-none mobile-top-menu"">
                                <a class=""home"" href=""{Microservice.Me.Url("/under")}"" data-redirect=""ajax""><i class=""fas fa-home""></i></a>                                
                                {await new GlobalSearchView(new ViewModel.GlobalSearch()).Render()}
                            </div>
                            <div class=""d-none d-md-block"">                                
                                {await new BreadcrumbWrapperView(new ViewModel.BreadcrumbWrapper()).Render()}
                            </div>";
            }

            return string.Empty;
        }

        private async Task<string> GenerateLeftMenu()
        {
            if (LeftMenu.HasValue())
            {
                var result = $@"<div class=""side-bar d-none d-lg-flex p-0"">
                    { await new SideBarTopView(new ViewModel.SideBarTopModule()).Render()}                    
                    {await GenerateSideMenu()}
                    { await new FooterView(new ViewModel.Footer()).Render()}
                </div>";

                return result;
            }

            return string.Empty;
        }

        private static async Task<string> GenerateSideMenu()
        {
            var sideMenu = new ViewModel.FeaturesSideMenu
            {
                Markup = AuthroziedFeatureInfo.RenderMenu(Website.FeatureContext.ViewingFeature).ToString()
            };

            return await new FeaturesSideMenuView(sideMenu).Render();
        }

        private string GenerateLeftDownMenu()
        {
            if (LeftMenu.HasValue())
            {
                var result = $@"<div class=""task-bar d-none d-lg-flex p-0"">
                                    <button type=""button"" id=""taskBarCollapse"" class=""navbar-btn d-none d-lg-block"">
                                        <i class=""fa fa-chevron-right"" aria-hidden=""true""></i>
                                    </button>
                                    <iframe id=""taskiFram"" style=""height: 100%;"" src=""{Microservice.Of("Tasks").Url()}widget-my-priority/{User.GetEmail().Split("@")[0].RemoveFrom(".")}"" sandbox=""allow-forms allow-scripts allow-same-origin allow-popups allow-top-navigation""></iframe>
                                </div>";

                return result;
            }

            return string.Empty;
        }

        protected abstract Task<string> RenderBodyAjax();
    }
}
