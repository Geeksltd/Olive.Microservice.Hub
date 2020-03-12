using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class FeaturesSideMenuController : BaseController
    {
        [NonAction, OnBound]
        public async Task OnBound(vm.FeaturesSideMenu info)
        {
            info.Markup = (AuthroziedFeatureInfo.RenderMenu(Website.FeatureContext.ViewingFeature)).ToString();
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.FeaturesSideMenuController))]
    public partial class FeaturesSideMenu : IViewModel
    {
        [ReadOnly(true)]
        public string Markup { get; set; }
    }
}