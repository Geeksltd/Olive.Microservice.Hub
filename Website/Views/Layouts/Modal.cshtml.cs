using Olive.Mvc;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public abstract class ModalLayout<TViewModel> : ModalContainerLayout<TViewModel> where TViewModel : IViewModel
    {
        protected ModalLayout(TViewModel viewModel) : base(viewModel)
        {

        }

        public override async Task<string> Render()
        {
            if (Olive.Context.Current.Request().IsAjaxCall())
            {
                var result = $@"
                                <main class=""hub-service"">
                                    <div class=""container-fluid"">
                                        <div class=""page"">
                                            <div class=""content""> 
                                                {await RenderBodyAjax()} 
                                            </div>
                                        </div>
                                    </div>
                                    {Html.RegisterStartupActions()}
                                </main>";

                return result;
            }
            return await base.Render();
        }
    }
}
