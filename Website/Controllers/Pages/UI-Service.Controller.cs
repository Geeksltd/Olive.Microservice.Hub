using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UIServiceController : BaseController
    {
        [Route("/UI/Service/{service}")]
        public async Task<ActionResult> Index(vm.ServiceView info)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Index("Login", new { ReturnUrl = Url.Current() }));
            }

            ViewData["LeftMenu"] = "FeaturesSideMenu";

            return await View<UIServiceView>(info);
        }

        [NonAction, OnBound]
        public async Task OnBound(vm.ServiceView info)
        {
            if (info.Url.HasValue())
            {
                // Load Javascript file
                JavaScript(new JavascriptService("hub", "go", info.DestinationUrl, info.Item.UseIframe));
            }
        }

        protected override async Task<bool> AuthorizeRequestParams(ActionExecutingContext context)
        {
            if (!(User.Identity.IsAuthenticated))
                return false;

            return await base.AuthorizeRequestParams(context);
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class ServiceView : IViewModel
    {
        [ReadOnly(true)]
        public string Url { get; set; }

        public string ActualRelativeUrl => Url.ToLower().TrimStart($"/{ Item.Name.ToLower()}/");

        public string DestinationUrl => Item.GetHubImplementationUrl(ActualRelativeUrl);

        [FromRequest("service")]
        [ValidateNever]
        public Service Item { get; set; }
    }
}