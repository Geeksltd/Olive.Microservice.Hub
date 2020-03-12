using Olive.Mvc;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIServiceView : DefaultLayout<ViewModel.ServiceView>
    {
        public UIServiceView(ViewModel.ServiceView vm) : base(vm) { }

        protected override Task<string> RenderBodyAjax()
        {
            var result = $@"<form data-module=""ServiceView"" method=""post"" action=""{Url.Current()}"">                               
                               {Html.StartupActionsJson()}
                               <div class=""view-body"">
                               </div>
                            </form>";

            return Task.Run(() => result);
        }
    }
}
