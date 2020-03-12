using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIFeatureView : BasePage<ViewModel.FeatureView>
    {
        public UIFeatureView(ViewModel.FeatureView vm) : base(vm) { }

        public override async Task<string> Render()
        {
            var vm = ViewBag.Info as ViewModel.FeatureView;

            return $"{await new FeatureView(vm).Render()}";
        }
    }
}
