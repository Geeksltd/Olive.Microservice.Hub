using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Mvc;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class BoardViewController : BaseController
    {
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    [BindingController(typeof(Olive.Hub.BoardViewController))]
    public partial class BoardView : IViewModel
    {
        public string FeatureId { get; set; }

        [ValidateNever]
        public Board Item { get; set; }
    }
}