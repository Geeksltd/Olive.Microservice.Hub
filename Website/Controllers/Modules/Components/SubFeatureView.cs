using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class SubFeatureViewController : BaseController
    {
        [NonAction, OnBound]
        public async Task OnBound(vm.SubFeatureView info)
        {
            // Load Javascript file
            JavaScript(new JavascriptService("hub", "go", info.RedirectUrl, info.Item.UseIframe));
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
    [BindingController(typeof(Olive.Hub.SubFeatureViewController))]
    public partial class SubFeatureView : IViewModel
    {
        [ReadOnly(true)]
        public string SubFeatureImplementationUrl { get; set; }

        public string RedirectUrl => Item.ToHubSubFeatureUrl(SubFeatureImplementationUrl);

        [ValidateNever]
        public Feature Item { get; set; }
    }
}