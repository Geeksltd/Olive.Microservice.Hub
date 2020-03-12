using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Olive.Mvc;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Olive.Hub
{
    [Authorize(Roles = "Employee")]
    [EscapeGCop("Auto generated code.")]
#pragma warning disable
    public partial class UIPersonBoardController : BaseController
    {
        [Route("person/{featureId}")]
        public async Task<ActionResult> Index(vm.BoardView info)
        {
            ViewBag.Info = info;
            ViewData["LeftMenu"] = "FeaturesSideMenu";

            //return View(ViewBag);
            return await View<BoardView>(info);
        }

        [NonAction, OnPreBound]
        public async Task OnBinding(vm.BoardView info)
        {
            info.Item = Board.Parse("Person");
        }
    }
}