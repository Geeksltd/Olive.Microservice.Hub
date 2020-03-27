using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class SubFeatureView : DefaultLayout<ViewModel.SubFeatureView>
    {
        public SubFeatureView(ViewModel.SubFeatureView vm) : base(vm)
        {
        }

        protected override Task<string> RenderBodyAjax()
        {
            var result = $@"                    
                            <form data-module=""SubFeatureView"" method=""post"" action=""{Url.Current()}"" class=""feature-frame-view"">                               
                               {Html.StartupActionsJson()}
                               <div class=""view-body"">
                               </div>
                            </form>";

            return Task.Run(() => result);
        }
    }
}
