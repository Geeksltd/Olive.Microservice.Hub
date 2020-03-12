using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Olive;
using Olive.Entities;
using Olive.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class SSOController : BaseController
    {
        [Route("sso")]
        public async Task<ActionResult> Index(vm.SingleSignOn info)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Index("Login", new { ReturnUrl = Url.Current() }));
            }

            return await View<SingleSignOnView>(info);
        }

        [NonAction, OnBound]
        public async Task OnBound(vm.SingleSignOn info)
        {
            info.Token = CreateToken(info.Ticket);

            info.Items = await GetSource(info)
                .Select(item => new vm.SingleSignOn.ListItem(item)).ToList();

            // Prepare apps
            await PrepareApps(info);
        }

        [NonAction]
        async Task<IEnumerable<Service>> GetSource(vm.SingleSignOn info)
        {
            IEnumerable<Service> result = Service.All.Where(x => x.InjectSingleSignon);

            return result;
        }
    }
}

namespace ViewModel
{
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class SingleSignOn : IViewModel
    {
        [ReadOnly(true)]
        public string Token { get; set; }

        [ReadOnly(true)]
        public List<ListItem> Items = new List<ListItem>();

        public partial class ListItem : IViewModel
        {
            public ListItem(Service item) => Item = item;

            [ValidateNever]
            public Service Item { get; set; }

            public string ServiceUrl => Item.GetAbsoluteImplementationUrl("@Services/SSO.ashx");
        }
    }
}