using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class FeaturesTopMenuController : BaseController
    {
        [NonAction, OnPreBound]
        public async Task OnPreBound(vm.FeaturesTopMenu info)
        {
            // Set ViewingFeature
            info.ViewingFeature = Website.FeatureContext.ViewingFeature;

            // Set the items
            info.Items = info.Parent.Children;

            if (info.Parent.ImplementationUrl.HasValue())
            {
                // Include the parent if it has implementation
                info.Items = new[] { info.Parent }.Union(info.Items);
            }
        }

        [NonAction, OnBound]
        public async Task OnBound(vm.FeaturesTopMenu info)
        {
            info.ActiveItem = GetActiveItem(info);
        }

        [NonAction]
        public string GetActiveItem(vm.FeaturesTopMenu info)
        {
            return info.Items.Reverse().FirstOrDefault(f => f.WithAllChildren().Contains(info.ViewingFeature))?.ID.ToString();

            return null;
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.FeaturesTopMenuController))]
    public partial class FeaturesTopMenu : IViewModel
    {
        [ReadOnly(true)]
        [ValidateNever]
        public Feature Parent { get; set; }

        [ReadOnly(true)]
        [ValidateNever]
        public Feature ViewingFeature { get; set; }

        [ReadOnly(true)]
        public IEnumerable<Feature> Items { get; set; }

        public string ActiveItem { get; set; }
    }
}