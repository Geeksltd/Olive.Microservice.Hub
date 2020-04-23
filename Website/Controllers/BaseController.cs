using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Olive.Mvc;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Olive.Hub
{
    public class BaseController : Olive.Mvc.Controller
    {
        public BaseController()
        {
            ApiClient.FallBack += arg => Notify(arg.Args.FriendlyMessage, false);
        }

        [NonAction]
        public new ActionResult Unauthorized() => Redirect("/login");

        protected override string GetDefaultBrowserTitle(ActionExecutingContext context)
            => Microservice.Me.Name + " > " + base.GetDefaultBrowserTitle(context);

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["ExecutionStart"] = LocalTime.Now;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var start = (DateTime)ViewData["ExecutionStart"];
            Log.Info("Finished executing " + context.ActionDescriptor.DisplayName + " in " + LocalTime.Now.Subtract(start).ToNaturalTime());
        }

        public async Task<ContentResult> View<TView>(IViewModel model)
        {
            return Content(await model.Render<TView>(ViewData), "text/html");
        }

        public async Task<ContentResult> View<TView>(IViewModel model, IViewModel second)
        {
            return Content(await model.Render<TView>(ViewData, second), "text/html");
        }
    }
}

namespace ViewComponents
{
    public abstract class ViewComponent : Olive.Mvc.ViewComponent { }
}