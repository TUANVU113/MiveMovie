using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MineMovie_Nhom7_CNPM.Models.ViewModels
{
    public class MovieDetailsVM
    {
        public int IdPhim { get; set; }
        public string TenPhim { get; set; }
        public string MoTa { get; set; }
        public TimeSpan? ThoiLuong { get; set; }
        public string QuocGia { get; set; }
        public string DaoDien { get; set; }
        public string HinhAnh { get; set; }
        public string Trailer { get; set; }
        public int? NamPh { get; set; }
        public PHIM Phim { get; set; }
        public bool IsFavorite { get; set; }
    }

}