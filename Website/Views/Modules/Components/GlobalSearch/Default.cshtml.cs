using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class GlobalSearchView : BasePage<ViewModel.GlobalSearch>
    {
        public GlobalSearchView(ViewModel.GlobalSearch vm) : base(vm)
        {
        }

        public override Task<string> Render()
        {
            var viewHtml = $@"                    
                            <div data-module=""GlobalSearch"" class=""no-print global-search"">
                               {Html.StartupActionsJson()}
                               <input type=""text"" name=""searcher""
                               placeholder=""Search...""
                               class=""form-control global-search""
                               data-search-source=""{Model.GetSearchSources()}"" />
                            </div>";

            return Task.Run(() => viewHtml);
        }
    }
}
