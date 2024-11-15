using MineMovie_Nhom7_CNPM.Models.ViewModels;
using MineMovie_Nhom7_CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MineMovie_Nhom7_CNPM.Controllers
{
    public class AccountController : Controller
    {
        private readonly MINEMOVIEEntities _dbContext;

        public AccountController()
        {
            _dbContext = new MINEMOVIEEntities();
        }

        // Action đăng ký
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usernameParam = new SqlParameter("@tentk", model.Username);
                var emailParam = new SqlParameter("@gmail", model.Email);
                var passwordParam = new SqlParameter("@mk", model.Password);
                var avatarParam = new SqlParameter("@ava", model.Avatar ?? string.Empty);

                try
                {
                    await _dbContext.Database.ExecuteSqlCommandAsync("EXEC procDangKiNguoiDung @tentk, @gmail, @mk, @ava",
                                                                    usernameParam, emailParam, passwordParam, avatarParam);
                    ViewBag.SuccessMessage = "Đăng ký thành công!";
                    return RedirectToAction("Login");
                }
                catch (SqlException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        // Action đăng nhập
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usernameParam = new SqlParameter("@tentk", model.Username);
                var passwordParam = new SqlParameter("@mk", model.Password);

                try
                {
                    var loginResult = await _dbContext.Database.SqlQuery<string>(
                        "EXEC procDangNhapNguoiDung @tentk, @mk",
                        usernameParam, passwordParam
                    ).FirstOrDefaultAsync();

                    if (loginResult != null) // Login success
                    {
                        var usernameParamForUserInfo = new SqlParameter("@tentk", model.Username);

                        // Fetch user information
                        var userInfo = await _dbContext.Database.SqlQuery<NGUOI_DUNG>(
                            "EXEC procThongTinNguoiDung @tentk",
                            usernameParamForUserInfo
                        ).FirstOrDefaultAsync();

                        if (userInfo != null)
                        {
                            // Store necessary user information in the session
                            Session["UserID"] = userInfo.ID_ND;
                            Session["Username"] = userInfo.TEN_ND;
                            Session["Role"] = userInfo.CHUC_VU;

                            // Redirect based on user role
                            if (userInfo.CHUC_VU == "Admin")
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home", new { area = "User" });
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng.");
                    }
                }
                catch (SqlException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var usernameParam = new SqlParameter("@tentk", model.Username);
        //        var passwordParam = new SqlParameter("@mk", model.Password);

        //        try
        //        {
        //            var loginResult = await _dbContext.Database.SqlQuery<string>(
        //                "EXEC procDangNhapNguoiDung @tentk, @mk",
        //                usernameParam, passwordParam
        //            ).FirstOrDefaultAsync();

        //            if (loginResult != null) // Login success
        //            {
        //                var usernameParamForUserInfo = new SqlParameter("@tentk", model.Username);

        //                // Fetch user information
        //                var userInfo = await _dbContext.Database.SqlQuery<NGUOI_DUNG>(
        //                    "EXEC procThongTinNguoiDung @tentk",
        //                    usernameParamForUserInfo
        //                ).FirstOrDefaultAsync();

        //                if (userInfo != null)
        //                {
        //                    // Store necessary user information in the session
        //                    Session["Role"] = userInfo.CHUC_VU;

        //                    // Redirect based on user role
        //                    if (userInfo.CHUC_VU == "Admin")
        //                    {
        //                        return RedirectToAction("Index", "Home", new { area = "Admin" });
        //                    }
        //                    else
        //                    {
        //                        return RedirectToAction("Index", "Home", new { area = "User" });
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng.");
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            ModelState.AddModelError("", ex.Message);
        //        }
        //    }
        //    return View(model);
        //}
    }
}