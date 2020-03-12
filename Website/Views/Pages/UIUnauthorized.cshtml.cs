using Olive.Mvc;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIUnauthorizedView : DefaultLayout<ViewModel.UnauthorizedAccess>
    {
        public UIUnauthorizedView(ViewModel.UnauthorizedAccess vm) : base(vm) { }

        protected override Task<string> RenderBodyAjax()
        {
            var result = $@"<form data-module=""UnauthorizedAccess"" method=""post"" action=""{Url.Current()}"" class=""error unauthorized"">   
                           {Html.StartupActionsJson()}
                           <div class=""view-body"">
                              <div class=""form-group row"">
                                 <div class=""group-control"">
                                    <h2>Access denied!</h2>
                                    <p>
                                       You do not appear to have access to
                                       <b>
                                          {info.Item.Title}
                                       </b>
                                       .
                                    </p>
                                    <p>
                                       In case your session is expired, try
                                       <a href='/login'>logging in</a>
                                       again.
                                    </div>
                                 </div>
                              </div>
                           </form>";

            return Task.Run(() => result);
        }
    }
}
