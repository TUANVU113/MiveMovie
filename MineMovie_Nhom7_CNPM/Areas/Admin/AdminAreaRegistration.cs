using System.Web.Mvc;

namespace MineMovie_Nhom7_CNPM.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MineMovie_Nhom7_CNPM.Areas.Admin.Controllers" }
            );
        }
    }
}