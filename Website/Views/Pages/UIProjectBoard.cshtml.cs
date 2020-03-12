using System.Threading.Tasks;

namespace Olive.Hub
{
    public class UIProjectBoardView : BasePage<ViewModel.BoardView>
    {
        public UIProjectBoardView(ViewModel.BoardView vm) : base(vm) { }

        public override async Task<string> Render()
        {
            var vm = ViewBag.Info as ViewModel.BoardView;

            return $"{await new BoardView(vm).Render()}";
        }
    }
}
