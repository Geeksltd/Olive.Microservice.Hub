using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class SingleSignOnView : DefaultLayout<ViewModel.SingleSignOn>
    {
        public SingleSignOnView(ViewModel.SingleSignOn vm) : base(vm) { }


        protected override Task<string> RenderBodyAjax()
        {
            var result = $@"<form data-module=""SingleSignOn"" method=""get"" action=""{Url.Current()}"" data-redirect=""ajax"">
                               {Html.StartupActionsJson()}
                               <h2>Logging in to other apps...</h2>
                               {Model.Errors.Raw()}
                            {GenerateChild()}
                            </form>

                            <script type=""text/javascript"">
                                 // Wait time
                                 function waitTime() {{ if (screen.width < 500) return 5000; else return 2000; }}
     
                                 // Redirect
                                 setTimeout(function() {{ window.location.href = '/' }}, waitTime())
                            </script>";

            return Task.Run(() => result);
        }

        private string GenerateChild()
        {
            if (Model.Items.Any())
                return $"<div class='list-items'>{GenerateItems()}</div>";
            else
                return @"<div class=""empty-list""> There are no services to display. s</div>";
        }

        private string GenerateItems()
        {
            var items = new StringBuilder();

            foreach (var listItem in Model.Items)
            {
                var item = listItem.Item;

                items.Append($@"<div class='item'>
                                     <div style='display:block; float:left; margin:15px; width:150px; height:150px;border:1px solid gray;'>
                                        {item}
                                        <iframe style='width:150px; height:150px; border: none' src='{listItem.ServiceUrl}?ticket={info.Ticket}'> </iframe>
                                     </div>
                                  </div>");
            }

            return items.ToString();
        }
    }
}
