using MineMovie_Nhom7_CNPM.Models;
using MineMovie_Nhom7_CNPM.Models.ViewModels;
using MineMovie_Nhom7_CNPM.Filter;
using MineMovie_Nhom7_CNPM.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Diagnostics;

namespace MineMovie_Nhom7_CNPM.Controllers
{
    public class MoviesController : Controller
    {
        MINEMOVIEEntities db = new MINEMOVIEEntities();
        //Index
        public ActionResult Index(string search = "")
        {
            //Sử dụng procedure procTimKiemPhim để có thể tìm từ khoá tên phim tiếng Việt hiệu quả hơn
            List<procTimKiemPhim_Result> dsphim = db.procTimKiemPhim(search).ToList();

            if (dsphim == null || !dsphim.Any())
            {
                ViewBag.Message = "Không tìm thấy phim nào.";
            }

            ViewBag.Search = search;
            return View(dsphim);
        }

        private readonly MovieService _movieService;
        public MoviesController() { _movieService = new MovieService(); }
        public async Task<ActionResult>  Details(int id)
        {
            var p = await db.CHI_TIET_PHIM.FindAsync(id);
            //CHI_TIET_PHIM p = db.CHI_TIET_PHIM.Where(row => row.ID_PHIM == id).FirstOrDefault();
            var movieDetails = await _movieService.GetMovieDetailsAsync(p.IMDB_ID);

            //Cụm code ở dưới lần lượt để truy xuất rating của imdb, rotten tomatoes và metacritic -> Đưa vào viewbag -> view Details để hiển thị
            var imdbRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Internet Movie Database")?.Value ?? "Không có thông tin";
            var rtRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Rotten Tomatoes")?.Value ?? "Không có thông tin";
            var metacriticRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Metacritic")?.Value ?? "Không có thông tin";
            ViewBag.ImdbRating = imdbRating;
            ViewBag.RtRating = rtRating;
            ViewBag.MetacriticRating = metacriticRating;

            bool isFavorite = false;
            if (Session["UserID"] != null) // Kiểm tra nếu người dùng đã đăng nhập
            {
                var userId = (int)Session["UserID"];
                isFavorite = await db.KH_PHIM_DSYT.AnyAsync(f => f.ID_PHIM == id && f.DS_YEU_THICH.ID_ND == userId);
            }

            var viewModel = new MovieDetailsVM 
            { 
                IdPhim = p.ID_PHIM,
                TenPhim = p.TEN_PHIM,
                MoTa = p.MO_TA,
                ThoiLuong = p.THOI_LUONG,
                QuocGia = p.QUOC_GIA,
                DaoDien = p.DAO_DIEN,
                HinhAnh = p.HINH_ANH,
                Trailer = p.TRAILER,
                NamPh = p.NAM_PH,
                Phim = p.PHIM,
                IsFavorite = isFavorite
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<JsonResult> ToggleFavoriteAjax(int id)
        {
            var userId = (int?)Session["UserID"];
            if (userId == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để thêm phim vào danh sách yêu thích." });
            }

            var favoriteList = await db.DS_YEU_THICH.FirstOrDefaultAsync(ds => ds.ID_ND == userId);
            if (favoriteList == null)
            {
                return Json(new { success = false, message = "Danh sách yêu thích không tồn tại." });
            }

            var movieInList = await db.KH_PHIM_DSYT
                .FirstOrDefaultAsync(k => k.ID_DS == favoriteList.ID_DS && k.ID_PHIM == id);

            if (movieInList != null)
            {
                // Nếu đã có trong danh sách yêu thích, xóa
                db.KH_PHIM_DSYT.Remove(movieInList);
                await db.SaveChangesAsync();
                return Json(new { success = true, isFavorite = false });
            }
            else
            {
                // Nếu chưa có trong danh sách yêu thích, thêm
                var newMovie = new KH_PHIM_DSYT
                {
                    ID_DS = favoriteList.ID_DS,
                    ID_PHIM = id,
                    NGAY_THEM = DateTime.Now
                };
                db.KH_PHIM_DSYT.Add(newMovie);
                await db.SaveChangesAsync();
                return Json(new { success = true, isFavorite = true });
            }
        }
    }
}