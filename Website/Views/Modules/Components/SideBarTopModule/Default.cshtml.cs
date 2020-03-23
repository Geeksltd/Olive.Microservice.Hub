using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class SideBarTopView : BasePage<ViewModel.SideBarTopModule>
    {
        public SideBarTopView(ViewModel.SideBarTopModule vm) : base(vm)
        {
        }

        public override async Task<string> Render()
        {
            return $@"
                        <form data-module=""SideBarTopModule"" method=""post"" action=""{Url.Current()}"">   
                            {Html.StartupActionsJson()}
                            <div class=""logo-wrapper"">
                                <div>
                                    <a name=""Logo"" class=""logo"" href=""/"" default-button=""true""><img src=""{Microservice.Me.Url()}{HubConfig.HubFile("/hub/image").Attributes["logo"]?.InnerText}"" alt=""Logo"" /></a>
                                </div>
                            </div>
                        {await new GlobalSearchView(new ViewModel.GlobalSearch()).Render()}
                        </form>";
        }

    }
}
