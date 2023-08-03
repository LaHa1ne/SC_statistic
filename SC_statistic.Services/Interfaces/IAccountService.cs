using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<bool>> IsUserExistsByLogin(RegistrationViewModel registrationViewModel);
        Task<BaseResponse<ClaimsIdentity>> Registration(RegistrationViewModel registrationViewModel);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel);
    }
}
