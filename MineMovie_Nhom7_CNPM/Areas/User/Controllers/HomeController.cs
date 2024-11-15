using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MineMovie_Nhom7_CNPM.Filter;
using MineMovie_Nhom7_CNPM.Models;


namespace MineMovie_Nhom7_CNPM.Areas.User.Controllers
{
    [RoleAuthorize("User")]
    public class HomeController : Controller
    {
        // GET: User/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}