//using Domain;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//using Olive;
//using Olive.Mvc;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Threading.Tasks;
//using vm = ViewModel;

//namespace Controllers
//{
//    [Authorize(Roles = "Director, HeadPM")]
//    [EscapeGCop("Auto generated code.")]
//#pragma warning disable
//    public partial class AdminServicesHealthCheckController : BaseController
//    {
//        [Route("admin/services-health-check")]
//        public async Task<ActionResult> Index(vm.ServicesHealthCheckTiles info)
//        {
//            ViewData["LeftMenu"] = "FeaturesSideMenu";

//            return await View<ServicesHealthCheckTileView>(info);
//        }

//        [NonAction, OnBound]
//        public async Task OnBound(vm.ServicesHealthCheckTiles info)
//        {
//            info.Items = await GetSource(info)
//                .Select(item => new vm.ServicesHealthCheckTiles.ListItem(item)).ToList();
//        }

//        [NonAction]
//        async Task<IEnumerable<Service>> GetSource(vm.ServicesHealthCheckTiles info)
//        {
//            IEnumerable<Service> result = Service.All;

//            return result;
//        }
//    }
//}

//namespace ViewModel
//{
//    [EscapeGCop("Auto generated code.")]
//#pragma warning disable
//    public partial class ServicesHealthCheckTiles : IViewModel
//    {
//        [ReadOnly(true)]
//        public List<ListItem> Items = new List<ListItem>();

//        public partial class ListItem : IViewModel
//        {
//            public ListItem(Service item) => Item = item;

//            [ValidateNever]
//            public Service Item { get; set; }
//        }
//    }
//}