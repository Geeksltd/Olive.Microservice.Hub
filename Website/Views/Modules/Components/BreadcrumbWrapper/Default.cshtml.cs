using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class BreadcrumbWrapperView : BasePage<ViewModel.BreadcrumbWrapper>
    {
        public BreadcrumbWrapperView(ViewModel.BreadcrumbWrapper vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"
                    <div data-module=""BreadcrumbWrapper"" class=""no-print"">
                       {Html.StartupActionsJson()}
                       <nav aria-label=""breadcrumb"">
                          <ol class=""breadcrumb"">
                          </ol>
                       </nav>
                    </div>";

            return Task.Run(() => result);
        }
    }
}
