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
    public partial class FeaturesTopMenuWrapperController : BaseController
    {
        [NonAction, OnBound]
        public async Task OnBound(vm.FeaturesTopMenuWrapper info)
        {
            info.Markup = (await AuthroziedFeatureInfo.RenderMenuJson()).ToString();

            info.IsVisible = User.Identity.IsAuthenticated;
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.FeaturesTopMenuWrapperController))]
    public partial class FeaturesTopMenuWrapper : IViewModel
    {
        [ReadOnly(true)]
        public bool IsVisible { get; set; }

        [ReadOnly(true)]
        public string Markup { get; set; }
    }
}