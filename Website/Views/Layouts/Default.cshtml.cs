using Olive.Mvc;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public abstract class DefaultLayout<TViewModel> : DefaultContainerLayout<TViewModel> where TViewModel : IViewModel
    {
        protected DefaultLayout(TViewModel viewModel) : base(viewModel)
        {

        }

        public override async Task<string> Render()
        {
            if (Olive.Context.Current.Request().IsAjaxCall())
            {
                var result = $@"<main class=""hub-service"">
                            <input type=""hidden"" id=""page_meta_title"" value=""{ViewData["Title"]}"" />
                            {await RenderBodyAjax()}
                            {Html.RegisterDynamicScriptFiles()}
                            {GenerateHiddenAction()}
                            </main>";

                return result;
            }
            return await base.Render();
        }
    }
}
