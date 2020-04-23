using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Olive;
using System;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    partial class LoginController
    {
        async Task TryLogin(string email)
        {
            var user = await Database.FirstOrDefault<PeopleService.UserInfo>(p => p.Email == email);

            if (user == null)
            {
                Log.Error(new Exception("Null user!"), "****** User is null for email " + email);

                throw new Exception(@"<li>Google did not supply us your email address (due to security restrictions you have set with them).</li>
                        <li>The email address you logged in with [to Google] is not registered in our database.</li>");
            }

            if (!user.IsActive)
                throw new Exception("<li>Your account is currently deactivated. It might be due to security concerns on your account. Please contact the system administrator to resolve this issue. We apologise for the inconvenience.</li>");

            await user.LogOn();
        }

        [HttpGet, Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                if (remoteError.HasValue())
                    return await Error($"Error from external provider: {remoteError}");

                var info = await HttpContext.AuthenticateAsync();

                if (info == null || !info.Succeeded)
                    return Redirect($"/login?returnUrl={returnUrl}");

                var email = info.Principal.GetEmail();

                if (email.IsEmpty())
                    return await Error("Google did not return your email to us.");

                try
                {
                    var user = Database.FirstOrDefault<PeopleService.UserInfo>(f => f.Email == email);
                    if (user == null) return Redirect("/login");

                    await TryLogin(email);
                }
                catch (Exception ex)
                {
                    return await Error(ex.Message);
                }

                return Redirect($"/SSO?returnUrl={returnUrl}");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        [HttpGet, Route("logout")]
        public async Task<IActionResult> Logout(vm.LoginForm _)
        {
            await HttpContext.SignOutAsync();
            return Redirect(Microservice.Of("Dashboard").Url("/login/logout.aspx"));
        }

        async Task<ActionResult> Error(string message)
        {
            var manual = new vm.ManualLogin();
            await TryUpdateModelAsync(manual);

            var login = new vm.LoginForm { ErrorMessage = message };
            await TryUpdateModelAsync(login);

            return await Index(manual, login);
        }
    }
}