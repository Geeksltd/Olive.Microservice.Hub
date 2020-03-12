using Olive.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIFeaturechildrenView : DefaultLayout<ViewModel.ChildFeaturesList>
    {
        public UIFeaturechildrenView(ViewModel.ChildFeaturesList vm) : base(vm)
        {
        }


        protected override Task<string> RenderBodyAjax()
        {
            var vm = ViewBag.Info as ViewModel.ChildFeaturesList;

            var result = $@"<form data-module=""ChildFeaturesList"" method=""get"" action=""{Url.Current()}"" data-redirect=""ajax"" class=""feature-children"">
                               {Html.StartupActionsJson()}
                               <div class=""search"">
                                  <div class=""form-group justify-content-center d-none d-lg-flex d-xl-flex"">
                                     <div class=""group-control"">
                                        <input type=""text"" id=""InstantSearch"" name=""InstantSearch"" class=""form-control"" placeholder=""Search..."" />
                                     </div>
                                  </div>
                               </div>
                            {GenerateChild()}
                            </form>";

            return Task.Run(() => result);
        }

        private string GenerateChild()
        {
            if (Model.Items.Any())
            {
                return $"<div class='list-items'>{GenerateItems()}</div>";
            }
            else
                return @"<div class=""empty-list""> There are no features to display.</div>";
        }

        private IEnumerable<string> GenerateItems()
        {
            foreach (var listItem in Model.Items)
            {
                var item = listItem.Item;

                yield return $@"<div class='item'>
                                 <a name=""Title"" class=""feature-button olive-instant-search-item badge-number"" href=""{item.LoadUrl}"" id=""{item.ID}"" {"data-redirect='ajax'".OnlyWhen(!item.UseIframe).Raw()} data-badgeurl=""{item.GetBadgeUrl()}"" data-service=""{item.Service?.Name}"" style=""color:{item.GetColour()};"">
                                    <i class=""{item.GetIcon()}"" aria-hidden=""true""></i>
                                    {item.GetTitle(Model.Parent)}
                                    <small>
                                       {item.GetDescription()}
                                    </small>
                                </a>
                              </div>";
            }
        }
    }
}
