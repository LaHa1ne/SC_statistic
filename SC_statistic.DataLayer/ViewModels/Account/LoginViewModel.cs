using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин не указан")]
        public string Login { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 24 символов")]
        public string Password { get; set; } = null!;
    }
}
