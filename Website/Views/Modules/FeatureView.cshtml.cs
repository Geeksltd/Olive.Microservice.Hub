using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class FeatureView : BasePage<ViewModel.FeatureView>
    {
        public FeatureView(ViewModel.FeatureView vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"                    
                            <form data-module=""FeatureView"" method=""post"" action=""{Url.Current()}"" class=""feature-frame-view"">                               
                               {Html.StartupActionsJson()}
                               <div class=""view-body"">
                               </div>
                            </form>";

            return Task.Run(() => result);
        }
    }
}
