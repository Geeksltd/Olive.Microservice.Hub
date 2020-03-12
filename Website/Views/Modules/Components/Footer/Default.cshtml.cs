using System.Threading.Tasks;
using Olive.Mvc;

namespace Olive.Hub
{
    public class FooterView : BasePage<ViewModel.Footer>
    {
        public FooterView(ViewModel.Footer vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"                    
                            <form data-module=""Footer"" method=""post"" action=""{Url.Current()}"" class=""logout-box"">   
                                {Html.StartupActionsJson()}
                                <hr/>
                                <div>
                                    Hi {User?.Identity.Name}
                                </div>
                                <div>
                                    <a name=""SignOut"" href=""/logout"" default-button=""true"">Sign out</a>
                                </div>
                            </form>";

            return Task.Run(() => result);
        }
    }
}
