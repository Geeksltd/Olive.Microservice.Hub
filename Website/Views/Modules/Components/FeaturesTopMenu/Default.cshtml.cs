using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class FeaturesTopMenuView : BasePage<ViewModel.FeaturesTopMenu>
    {
        public FeaturesTopMenuView(ViewModel.FeaturesTopMenu vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"<ul class=""features-sub-menu"">
                            {GenerateChild()}
                               <li class=""{"active".OnlyWhen(Model.ActiveItem == "Parent") + " active-parent".OnlyWhen(AddParentClass())}"">
                                  <a data-toggle=""collapse"" data-target=""#Parent"" {"aria-expanded=true".OnlyWhen(AddParentClass())}>Parent</a>
                                  <ul id=""Parent"" class=""collapse {"show".OnlyWhen(AddParentClass())}"">
                                     <li class=""{("active".OnlyWhen(Model.ActiveItem == "Parent/Child") + " active-parent".OnlyWhen(Model.ActiveItem.OrEmpty().StartsWith("Parent/Child/")))}"">
                                        <a>Child</a>
                                     </li>
                                  </ul>
                               </li>
                            </ul>";

            return Task.Run(() => result);
        }

        private bool AddParentClass()
        {
            return Model.ActiveItem.OrEmpty().StartsWith("Parent/");
        }

        private string GenerateChild()
        {
            var items = new StringBuilder();

            foreach (var item in Model.Items)
            {
                items.Append($@"<li class=""{"active".OnlyWhen(Model.ActiveItem == (item.ID).ToStringOrEmpty()) + " feature-menu-item" + " feature-box".OnlyWhen(Model.ViewingFeature.ImplementationUrl.IsEmpty() && item.Parent == Model.ViewingFeature) + " active-parent".OnlyWhen(Model.ActiveItem.OrEmpty().StartsWith((item.ID).ToStringOrEmpty() + "/"))}"">
                                  <a href=""{item.LoadUrl}"" data-redirect='ajax' data-badgeurl=""{item.BadgeUrl}"" data-service=""{item.Service?.Name}"" class=""{"badge-number".OnlyWhen(item.BadgeUrl.HasValue())}"">{item.Title}</a>
                               </li>");
            }

            return items.ToString();
        }
    }
}
