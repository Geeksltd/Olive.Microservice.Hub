using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace Olive.Hub
{
    public interface IBasePage
    {
        Task<string> Render();
        void Initialize(ViewDataDictionary viewData);
    }
}
