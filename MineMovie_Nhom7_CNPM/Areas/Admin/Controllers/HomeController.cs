using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MineMovie_Nhom7_CNPM.Filter;

namespace MineMovie_Nhom7_CNPM.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [RoleAuthorize("Admin")]
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}