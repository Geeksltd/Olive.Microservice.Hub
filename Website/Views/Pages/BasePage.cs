using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Olive.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Olive.Hub
{

    public abstract class BasePage<TViewModel> : BaseView<TViewModel>, IBasePage where TViewModel : IViewModel
    {
        protected new readonly TViewModel Model;
        protected new TViewModel info => Model;

        protected IViewComponentHelper Component;
        protected IUrlHelper Url => Context.GetUrlHelper();

        protected IHtmlHelper Html = new BasicHtmlHelper();

        public new dynamic ViewBag => Context.ViewBag();

        public new HttpContext Context => Olive.Context.Current.Http();

        public override ClaimsPrincipal User => Olive.Context.Current.User();

        protected BasePage(TViewModel model, IViewComponentHelper componentHelper = null)
        {
            Model = model;
            Component = componentHelper;
        }

        public void Initialize(ViewDataDictionary viewData) => base.ViewData = new ViewDataDictionary<TViewModel>(viewData);


        public abstract Task<string> Render();

        public override Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
