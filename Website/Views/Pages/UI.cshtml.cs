using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIView : BasePage<ViewModel.FeatureView>
    {
        public UIView(ViewModel.FeatureView vm) : base(vm) { }

        public override async Task<string> Render()
        {
            var vm = ViewBag.Info as ViewModel.FeatureView;

            return $"{await new FeatureView(vm).Render()}";
        }
    }
}
