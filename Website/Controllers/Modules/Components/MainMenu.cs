using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Mvc;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class MainMenuController : BaseController
    {
        [NonAction, OnBound]
        public async Task OnBound(vm.MainMenu info)
        {
            info.ActiveItem = GetActiveItem(info);
        }

        [NonAction]
        public string GetActiveItem(vm.MainMenu info) => null;
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.MainMenuController))]
    public partial class MainMenu : IViewModel
    {
        public string ActiveItem { get; set; }
    }
}