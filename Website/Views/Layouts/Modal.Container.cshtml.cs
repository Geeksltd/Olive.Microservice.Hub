using System.Threading.Tasks;
using Olive.Mvc;


namespace Olive.Hub
{
    public abstract class ModalContainerLayout<TViewModel> : BasePage<TViewModel> where TViewModel : IViewModel
    {
        protected ModalContainerLayout(TViewModel model) : base(model)
        {
            TimeOut = Config.Get("Authentication: Timeout", defaultValue: 20).Minutes().TotalSeconds + 10;
        }

        public double TimeOut { get; set; }

        public override async Task<string> Render()
        {
            var appVersion = Config.Get("App.Resource.Version", "1");

            return $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset=""utf-8"" />
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <meta http-equiv=""refresh"" content=""{TimeOut}"">
                        <title>{ViewData["Title"]}</title>
                        <link rel='stylesheet' href=""styles/theme.min.css?v={appVersion}"" type='text/css' />
                    </head>
                    <body>
                        <script src=""lib/requirejs/require.js"" data-main=""/scripts/references.js?v={appVersion}""></script>
                        <service of=""hub"">
                            {RenderBodyAjax()}
                        </service>
                    </body>
                    </html>";
        }

        protected abstract Task<string> RenderBodyAjax();
    }
}
