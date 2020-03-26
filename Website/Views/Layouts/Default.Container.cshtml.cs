using System.Threading.Tasks;
using System.Web;
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

            var appVersion = Config.Get("App.Resource.Version", "1");

            var result = $@"
                            <!DOCTYPE html>
                            <html class=""hub-service"">
                            <head>
                                <meta charset=""utf-8"" />
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <meta http-equiv=""refresh"" content=""{TimeOut}"">
                                <title>{ViewData["Title"]}</title>
                                <link rel='stylesheet' href=""{Microservice.Me.Url()}styles/hub/hub.min.css?v={appVersion}"" type='text/css' />
                                <link rel='stylesheet' href=""{Microservice.Me.Url()}styles/theme.min.css?v={appVersion}"" type='text/css' />
                                <link rel=""icon"" media=""all"" type=""image/x-icon"" href=""{Microservice.Me.Url()}{HubConfig.HubFile("/hub/image").Attributes["favicon"]?.InnerText}"" />
                                <link rel=""shortcut icon"" href=""img/favicon.ico"">

                                <meta name=""apple-mobile-web-app-capable"" content=""yes"">
                                <meta name=""msapplication-starturl"" content=""/"">

                                <!-- Manifest.json-->
                                <link rel=""manifest"" href=""{Microservice.Me.Url()}manifest.json"">                                
                                <meta name=""theme-color"" content=""#42AAA9"">

                                <script type=""text/javascript"">
                                    window[""services""] = {Service.ToJson()?.Raw()}
                                </script>
                                <script src=""{Microservice.Me.Url()}lib/requirejs/require.js"" data-main=""{Microservice.Me.Url()}scripts/references.js?v={appVersion}""></script>
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
                                                    <main class=""hub-service"">
                                                        <input type=""hidden"" id=""page_meta_title"" value=""{ViewData["Title"]}"" />
                                                    {await RenderBodyAjax()}
                                                    {Html.RegisterDynamicScriptFiles()}
                                                    {GenerateHiddenAction()}
                                                    </main>
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

        protected string GenerateHiddenAction()
        {
            var startupActions = HttpUtility.HtmlEncode(Html.GetActionsJson().ToString().Unless("[]"));

            return startupActions.HasValue() ? $"<input id=\"Startup_Actions\" name=\"Startup.Actions\" type=\"hidden\" value=\"{ startupActions}\" />" : string.Empty;
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
