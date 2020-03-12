using System.Threading.Tasks;

namespace Olive.Hub
{
    public class MainMenuView : BasePage<ViewModel.MainMenu>
    {
        public MainMenuView(ViewModel.MainMenu vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = @"<ul class=""nav navbar-nav dropped-submenu"">
                            </ul>";

            return Task.Run(() => result);
        }
    }
}
