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
                throw new Exception("<li>Your account is currently deactivated. It might be due to security concerns on your account. Please contact the system administrator to resolve this issue. We apologies for the inconvenience.</li>");

            await user.LogOn();
        }

        [HttpGet, Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                if (remoteError.HasValue())
                    return await Error($"Error from external provider: {remoteError}");

                Log.Info("Authenticating ...");
                var info = await HttpContext.AuthenticateAsync();
                Log.Info("Authenticated ...");

                Log.Info("Download google.com");
                var content = await "http://www.google.com".AsUri().Download();
                Log.Info(content);

                foreach (var header in Request.Headers)
                    Log.Info(">>>>>" + header.Key + " " + header.Value);


                Log.Info(">>>>> RemoteIpAddress " + Request.HttpContext.Connection.RemoteIpAddress);
                Log.Info(">>>>> Scheme " + Request.Scheme);
                Log.Info(">>>>> Host " + Request.Host);
                Log.Info(">>>>> Body " + await Request.Body.ReadAllText());

                Log.Info(">>>>> Cookies ");
                foreach (var item in Request.GetCookies())
                    Log.Info(">>>>> Cookie " + item.Key + " " + item.Value);

                Log.Info(">>>>> Info.Succeeded " + info.Succeeded);
                Log.Info(">>>>> Info.Principal " + info.Principal);
                Log.Info(">>>>> Info.Ticket " + info.Ticket);
                Log.Info(">>>>> Info.Failure " + info.Failure);

                Log.Info(">>>>> Info.Parameters ");
                foreach (var item in (info.Properties?.Parameters).OrEmpty())
                    Log.Info(item.Key + " " + item.Value);

                Log.Info(">>>>> Info.Items ");
                foreach (var item in (info.Properties?.Items).OrEmpty())
                    Log.Info(item.Key + " " + item.Value);

                Log.Info(">>>>> Info.Parameters.ExpiresUtc " + info.Properties?.ExpiresUtc);
                Log.Info(">>>>> Info.Parameters.IsPersistent " + info.Properties?.IsPersistent);
                Log.Info(">>>>> Info.Parameters.IssuedUtc " + info.Properties?.IssuedUtc);
                Log.Info(">>>>> Info.Parameters.RedirectUri " + info.Properties?.RedirectUri);

                // Log.Info(Newtonsoft.Json.JsonConvert.SerializeObject(info));

                if (info == null || !info.Succeeded)
                {
                    return Redirect($"/login?returnUrl={returnUrl}");
                }

                var issuer = info.Principal.GetFirstIssuer();
                var email = info.Principal.GetEmail();

                if (email.IsEmpty())
                {
                    return await Error("Google did not return your email to us.");
                }

                try
                {
                    var user = Database.FirstOrDefault<PeopleService.UserInfo>(f => f.Email == email);
                    if (user == null)
                        return Redirect("/login");

                    Console.WriteLine("*********************111 " + email);
                    await TryLogin(email);
                    Console.WriteLine("********************* " + email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("********************* ERROR: " + email);
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
            // throw new Exception();
            var manual = new vm.ManualLogin();
            await TryUpdateModelAsync(manual);

            var login = new vm.LoginForm { ErrorMessage = message };
            await TryUpdateModelAsync(login);

            return await Index(manual, login);
        }
    }
}