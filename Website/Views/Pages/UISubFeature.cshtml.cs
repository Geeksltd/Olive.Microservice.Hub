using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UISubFeatureView : BasePage<ViewModel.SubFeatureView>
    {
        public UISubFeatureView(ViewModel.SubFeatureView vm) : base(vm) { }

        public override async Task<string> Render()
        {
            var vm = ViewBag.Info as ViewModel.SubFeatureView;

            return $"{await new SubFeatureView(vm).Render()}";
        }
    }
}
