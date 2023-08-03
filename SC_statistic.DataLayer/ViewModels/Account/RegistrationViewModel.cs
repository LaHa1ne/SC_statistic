using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Логин не указан")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Логин должен содержать от 4 до 16 символов")]
        public string Login { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 24 символов")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не подтвержден")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
