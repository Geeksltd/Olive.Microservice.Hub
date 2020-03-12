using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Olive.Mvc;
using System;
using System.Threading.Tasks;

namespace Olive.Hub
{
    static class MarkupExtensions
    {
        public static Task<string> Render<TView>(this IViewModel info, ViewDataDictionary viewData)
        {
            var viewHtml = (IBasePage)Activator.CreateInstance(typeof(TView), new object[] { info });
            viewHtml.Initialize(viewData);
            return viewHtml.Render();
        }

        public static Task<string> Render<TView>(this IViewModel info, ViewDataDictionary viewData, IViewModel secondConstructorParameter)
        {
            var viewHtml = (IBasePage)Activator.CreateInstance(typeof(TView), new object[] { info, secondConstructorParameter });
            viewHtml.Initialize(viewData);
            return viewHtml.Render();
        }

        public static Task<string> Render<TView>(this IViewModel info, ViewDataDictionary viewData, IViewModel secondConstructorParameter, IViewModel thirdConstructorParameter)
        {
            var viewHtml = (IBasePage)Activator.CreateInstance(typeof(TView), new object[] { info, secondConstructorParameter, thirdConstructorParameter });
            viewHtml.Initialize(viewData);
            return viewHtml.Render();
        }

    }
}
