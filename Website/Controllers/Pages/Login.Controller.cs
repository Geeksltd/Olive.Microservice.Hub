using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Olive;
using Olive.Mvc;
using Olive.Security;
using System.ComponentModel;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class LoginController : BaseController
    {
        [Route("login/{item:Guid?}")]
        public async Task<ActionResult> Index(vm.ManualLogin manualLogin, vm.LoginForm loginForm)
        {
            if (Request.Param("returnUrl").IsEmpty())
            {
                return Redirect(Url.Index("Login", new { ReturnUrl = "/" }));
            }

            // Remove initial validation messages as well as unintended injected data
            ModelState.Clear();

            return await View<LoginView>(loginForm, manualLogin);
            //return Content(await new LoginView(loginForm, manualLogin) { ViewData = ViewData }.Render());
        }

        [HttpPost("LoginForm/LoginByGoogle")]
        public async Task<ActionResult> LoginByGoogle(vm.LoginForm info)
        {
            await OAuth.Instance.LoginBy("Google");

            return JsonActions(info);
        }

        [NonAction, OnBound]
        public async Task OnBound(vm.LoginForm info)
        {
            info.Item = info.Item ?? new User();

            // Clear cookies
            var alreadyDead = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = LocalTime.Today.AddDays(-1)
            };

            foreach (var c in Request.Cookies)
                Response.Cookies.Append(c.Key, string.Empty, alreadyDead);

            if (Request.IsGet()) await info.Item.CopyDataTo(info);
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class LoginForm : IViewModel
    {
        [ReadOnly(true)]
        public string ErrorMessage { get; set; }

        [ValidateNever]
        public User Item { get; set; }
    }
}