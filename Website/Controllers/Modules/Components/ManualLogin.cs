using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using PeopleService;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class ManualLoginController : BaseController
    {
        [HttpPost("ManualLogin/SimulateLogIn")]
        public async Task<ActionResult> SimulateLogIn(vm.ManualLogin info)
        {
            if (!(info.IsVisible))
                return new UnauthorizedResult();
            await info.CopyDataTo(info.Item);

            info.Item.Roles = info.RoleNames;

            await info.Item.LogOn();

            return Redirect(Request.Param("returnUrl"));
        }

        [NonAction, OnPreBinding]
        public async Task OnPreBinding(vm.ManualLogin info)
        {
            if (Request.IsGet())
            {
                // Set default roles
                info.RoleNames = HubConfig.HubFile("/hub/auth/testUser").Attributes["roles"].InnerText;
            }
        }

        [NonAction, OnBound]
        public async Task OnBound(vm.ManualLogin info)
        {
            info.Item = info.Item ?? new UserInfo();

            info.IsVisible = info.AllowManual;

            if (Request.IsGet()) await info.Item.CopyDataTo(info);

            info.DisplayName = HubConfig.HubFile("/hub/auth/testUser").Attributes["displayName"].InnerText;

            info.Email = HubConfig.HubFile("/hub/auth/testUser").Attributes["email"].InnerText;

            info.DisplayName_Visible = info.IsVisible;
            info.Email_Visible = info.IsVisible;
            info.RoleNames_Visible = info.IsVisible;

            TryValidateModel(info);

            // Set user ID for manual login
            if (!HubConfig.HubFile("/hub/auth/testUser").Attributes["id"].InnerText.IsEmpty())
                info.Item.ID = HubConfig.HubFile("/hub/auth/testUser").Attributes["id"].InnerText.To<Guid>();
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.ManualLoginController))]
    public partial class ManualLogin : IViewModel
    {
        [ReadOnly(true)]
        public bool IsVisible { get; set; }

        public bool AllowManual => Config.Get("Authentication:AllowManual", defaultValue: false);

        [ValidateNever]
        public UserInfo Item { get; set; }

        [CustomBound]
        public string DisplayName { get; set; }

        [CustomBound]
        [StringLengthWhen(nameof(Email_Visible), 100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string Email { get; set; }

        public string RoleNames { get; set; }

        [ReadOnly(true)]
        public bool DisplayName_Visible { get; set; }

        [ReadOnly(true)]
        public bool Email_Visible { get; set; }

        [ReadOnly(true)]
        public bool RoleNames_Visible { get; set; }
    }
}