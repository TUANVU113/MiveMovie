using MineMovie_Nhom7_CNPM.Models;
using MineMovie_Nhom7_CNPM.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MineMovie_Nhom7_CNPM.Controllers
{
    public class MoviesController : Controller
    {
        //Index
        public ActionResult Index(string search = "")
        {
            MINEMOVIEEntities db = new MINEMOVIEEntities();
            //Sử dụng procedure sp_TimKiemPhim để có thể tìm từ khoá tên phim tiếng Việt hiệu quả hơn
            List<sp_TimKiemPhim_Result> dsphim = db.sp_TimKiemPhim(search).ToList();

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
            MINEMOVIEEntities db = new MINEMOVIEEntities();
            CHI_TIET_PHIM p = db.CHI_TIET_PHIM.Where(row => row.ID_PHIM == id).FirstOrDefault();

            var movieDetails = await _movieService.GetMovieDetailsAsync(p.IMDB_ID);

            //Cụm code ở dưới lần lượt để truy xuất rating của imdb, rotten tomatoes và metacritic -> Đưa vào viewbag -> view Details để hiển thị
            var imdbRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Internet Movie Database")?.Value ?? "Không có thông tin";
            var rtRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Rotten Tomatoes")?.Value ?? "Không có thông tin";
            var metacriticRating = movieDetails.Ratings?.FirstOrDefault(r => r.Source == "Metacritic")?.Value ?? "Không có thông tin";
            ViewBag.ImdbRating = imdbRating;
            ViewBag.RtRating = rtRating;
            ViewBag.MetacriticRating = metacriticRating;

            return View(p);
        }
    }
}