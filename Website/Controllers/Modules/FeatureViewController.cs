using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class FeatureViewController : BaseController
    {
        [NonAction, OnBound]
        public async Task OnBound(vm.FeatureView info)
        {
            // Load Javascript file
            JavaScript(new JavascriptService("hub", "go", info.Item.LoadUrl, info.Item.UseIframe));

            if (info.Item?.ImplementationUrl.HasValue() == true && info.Item?.UseIframe == true)
            {
                // Load Javascript file
                JavaScript(new JavascriptService("featuresMenu", "show", info.Item.ID));
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
    [BindingController(typeof(Olive.Hub.FeatureViewController))]
    public partial class FeatureView : IViewModel
    {
        [ValidateNever]
        public Feature Item { get; set; }
    }
}