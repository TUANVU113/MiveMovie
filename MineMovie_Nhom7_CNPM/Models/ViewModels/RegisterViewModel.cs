using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MineMovie_Nhom7_CNPM.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; } // Đường dẫn ảnh đại diện
    }
}