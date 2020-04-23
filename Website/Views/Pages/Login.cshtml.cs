using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Olive.Mvc;

namespace Olive.Hub
{
    public class LoginView : DefaultLayout<ViewModel.LoginForm>
    {
        public LoginView(ViewModel.LoginForm loginForm, ViewModel.ManualLogin manual) : base(loginForm)
        {
            Manual = manual;
        }

        public ViewModel.ManualLogin Manual { get; }

        protected override async Task<string> RenderBodyAjax()
        {
            return $@"<div class=""login-page"">  
                            {await Manual.Render<ManualLoginView>(ViewData)}    
                            <form data-module=""LoginForm"" role=""form"" method=""post"" action=""{Url.Current()}"" data-validation-style=""tooltip"" class=""social-media-login"">        
                                {Html.StartupActionsJson()}
                                <div class='google-login-wrapper'>            
                                    <img name=""Logo"" src=""{HubConfig.HubFile("/hub/image").Attributes["geeksLogo"]?.InnerText}"" alt=""Logo"" style=""max-width: 70%;"" /> 
                                    <br /> 
                                    <img name=""FitSuite"" src=""{HubConfig.HubFile("/hub/image").Attributes["login"]?.InnerText}"" alt=""FitSuite"" style=""max-width: 50%;"" /> 
                                    <br /> <br />
                                <button type=""submit"" name=""LoginByGoogle"" class=""btn-social btn-google btn btn-primary btn btn-primary"" formaction='{Url.ActionWithQuery("LoginForm/LoginByGoogle")}' default-button=""true"" formmethod='post'><i class=""fas fa-user-lock""></i> Login by Google </button>
                                </div> <div class=""form-body"">
                                <div class=""form-group row"">
                                <div class=""group-control"">
                                {Model.ErrorMessage.Raw()}
                            </div></div></div>
                            </form></div>";
        }
    }
}
