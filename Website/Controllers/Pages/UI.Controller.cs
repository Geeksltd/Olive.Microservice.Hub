using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Olive.Mvc;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UIController : BaseController
    {
        [Route("/UI")]
        [Route("")]
        public async Task<ActionResult> Index(vm.FeatureView info)
        {
            ViewData["Title"] = "Geeks Access Hub";

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Index("Login", new { ReturnUrl = Url.Current() }));
            }

            ViewBag.Info = info;
            ViewData["LeftMenu"] = "FeaturesSideMenu";

            // return View(ViewBag);

            return await View<UIView>(info);
        }

        protected override async Task<bool> AuthorizeRequestParams(ActionExecutingContext context)
        {
            if (!(User.Identity.IsAuthenticated))
                return false;

            return await base.AuthorizeRequestParams(context);
        }
    }
}