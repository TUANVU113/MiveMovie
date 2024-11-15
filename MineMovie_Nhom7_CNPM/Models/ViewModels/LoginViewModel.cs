using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MineMovie_Nhom7_CNPM.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}