using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UIUnauthorizedController : BaseController
    {
        [Route("UI/Unauthorized/{feature}")]
        public async Task<ActionResult> Index(vm.UnauthorizedAccess info)
        {
            ViewData["LeftMenu"] = "FeaturesSideMenu";

            return await View<UIUnauthorizedView>(info);
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UnauthorizedAccess : IViewModel
    {
        [FromRequest("feature")]
        [ValidateNever]
        public Feature Item { get; set; }
    }
}