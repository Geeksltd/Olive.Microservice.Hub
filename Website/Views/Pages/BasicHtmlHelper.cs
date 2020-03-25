using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Olive.Hub
{
    /// <summary>
    /// Custom implementation of IHtmlHlper
    /// </summary>
    public class BasicHtmlHelper : IHtmlHelper
    {
        //private readonly IHtmlGenerator _htmlGenerator;

        //public BasicHtmlHelper(IHtmlGenerator htmlGenerator)
        //{
        //    if (htmlGenerator == null)
        //    {
        //        throw new ArgumentNullException(nameof(htmlGenerator));
        //    }

        //    _htmlGenerator = htmlGenerator;
        //}

        public dynamic ViewBag => throw new NotImplementedException();

        public ViewContext ViewContext { get; } = new ViewContext { HttpContext = Context.Current.Http() };

        #region Not Implemented yet

        public Html5DateRenderingMode Html5DateRenderingMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string IdAttributeDotReplacement => throw new NotImplementedException();

        public IModelMetadataProvider MetadataProvider => throw new NotImplementedException();

        public ViewDataDictionary ViewData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITempDataDictionary TempData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public UrlEncoder UrlEncoder => throw new NotImplementedException();

        public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol, string hostname, string fragment, object routeValues, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent AntiForgeryToken()
        {
            throw new NotImplementedException();
        }

        public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string DisplayName(string expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayText(string expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string Encode(object value)
        {
            throw new NotImplementedException();
        }

        public string Encode(string value)
        {
            throw new NotImplementedException();
        }

        public void EndForm()
        {
            throw new NotImplementedException();
        }

        public string FormatValue(object value, string format)
        {
            throw new NotImplementedException();
        }

        public string GenerateIdFromName(string fullName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType)
        {
            throw new NotImplementedException();
        }

        public string Id(string expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Label(string expression, string labelText, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string Name(string expression)
        {
            throw new NotImplementedException();
        }

        public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Password(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Raw(string value)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Raw(object value)
        {
            throw new NotImplementedException();
        }

        public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public string Value(string expression, string format)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IHtmlContent Hidden(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
            //return GenerateHidden(
            //    modelExplorer: null,
            //    expression: expression,
            //    value: value,
            //    useViewData: (value == null),
            //    htmlAttributes: htmlAttributes);
        }

        //protected virtual IHtmlContent GenerateHidden(
        //    ModelExplorer modelExplorer,
        //    string expression,
        //    object value,
        //    bool useViewData,
        //    object htmlAttributes)
        //{
        //    var tagBuilder = _htmlGenerator.GenerateHidden(
        //        ViewContext,
        //        modelExplorer,
        //        expression,
        //        value,
        //        useViewData,
        //        htmlAttributes);
        //    if (tagBuilder == null)
        //    {
        //        return HtmlString.Empty;
        //    }

        //    return tagBuilder;
        //}
    }
}
