using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class FeaturesSideMenuView : BasePage<ViewModel.FeaturesSideMenu>
    {
        public FeaturesSideMenuView(ViewModel.FeaturesSideMenu vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"
                    <div data-module=""FeaturesSideMenu"" class=""features-side-menu"">
                       {Html.StartupActionsJson()}
                       {info.Markup.Raw()}
                    </div>";

            return Task.Run(() => result);
        }
    }
}
