//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Olive;
//using Olive.Mvc;
//using System.Threading.Tasks;

//namespace Controllers
//{
//    [Authorize(Roles = "Director")]
//    [EscapeGCop("Auto generated code.")]
//#pragma warning disable
//    public partial class AdminController : BaseController
//    {
//        [Route("admin")]
//        public async Task<ActionResult> Index()
//        {
//            return Redirect(Url.Index("AdminFeatures"));

//            ViewData["LeftMenu"] = "FeaturesSideMenu";

//            return new EmptyResult();
//        }
//    }
//}