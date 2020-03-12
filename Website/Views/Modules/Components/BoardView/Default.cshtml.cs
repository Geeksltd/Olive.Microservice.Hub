using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Olive.Mvc;

namespace Olive.Hub
{
    public class BoardView : BasePage<ViewModel.BoardView>
    {
        public BoardView(ViewModel.BoardView vm) : base(vm) { }

        public override Task<string> Render()
        {
            var result = $@"
                        <div data-module=""BoardView"" class=""gridster-holder"">
                            {Html.StartupActionsJson()}
                            <div data-module='BoardView'>      
                                <div class='gridster'>
                                    <ul>
                                    {GenerateChilds()}
                                    </ul>
                                </div>
                            </div>
                            {Model.Item.GetRightWidget(User)?.RenderRightSide(info.FeatureId).Raw()}
                        </div>";

            return Task.Run(() => result);
        }

        private IEnumerable<string> GenerateChilds()
        {
            foreach (var widget in Model.Item.GetWidgets(User))
            {
                yield return $"<li>{widget.Render(info.FeatureId).Raw()}</li>";
            }
        }
    }
}
