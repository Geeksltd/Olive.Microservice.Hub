using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class ManualLoginView : BasePage<ViewModel.ManualLogin>
    {
        public ManualLoginView(ViewModel.ManualLogin manualLogin) : base(manualLogin)
        {
        }

        public override Task<string> Render()
        {
            var result = $@"
                    <form data-module=""ManualLogin"" role=""form"" method=""post"" action=""{Url.Current()}"" data-validation-style=""tooltip"" class=""manual-login"">
                     
                       {Html.StartupActionsJson()}
                       <h2>Bypass auth
                          <br/>
                          (dev time only)
                          <br/>
                          <br/></h2>
                       <div class=""form-body"">
                        {DisplayName()}
                        {DisplayEmail()}      
                        {DisplayRole()}
                       </div>
                       <div class=""buttons-row"">
                          <div class=""buttons"">
                             <button type=""submit"" name=""SimulateLogIn"" class=""btn btn-primary"" formaction='{Url.ActionWithQuery("ManualLogin/SimulateLogIn")}' default-button=""true"">Simulate log in</button>
                          </div>
                       </div>
                    </form>";

            return Task.Run(() => result);
        }

        private object DisplayRole()
        {
            if (Model.RoleNames_Visible)
            {
                return $@"<div class=""form-group row"">
                             <div class=""group-control"">
                                <textarea id=""RoleNames"" name=""RoleNames"" class=""form-control"" rows='10'>{Model.RoleNames}</textarea>
                             </div>
                          </div>";
            }

            return string.Empty;
        }

        private object DisplayEmail()
        {
            if (Model.Email_Visible)
            {
                return $@"<div class=""form-group row"">
                             <div class=""group-control"">
                                <input type=""text"" id=""Email"" name=""Email"" class=""form-control"" value=""{Model.Email}"" />
                             </div>
                          </div>";
            }

            return string.Empty;
        }

        private string DisplayName()
        {
            if (Model.DisplayName_Visible)
            {
                return $@"<div class=""form-group row"">
                             <div class=""group-control"">
                                <textarea id=""DisplayName"" name=""DisplayName"" class=""form-control"" rows=""5"">{Model.DisplayName}</textarea>
                             </div>
                          </div>";
            }

            return string.Empty;
        }
    }
}
