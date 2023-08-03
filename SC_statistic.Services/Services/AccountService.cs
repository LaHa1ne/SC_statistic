using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using SC_statistic.DataLayer.Helpers;
using SC_statistic.DataLayer.Responses;
using SC_statistic.DataLayer.ViewModels.Account;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services
{
    public class AccountService : IAccountService
    {
        protected readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<bool>> IsUserExistsByLogin(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var user = await _userRepository.GetByLogin(registrationViewModel.Login);
                return new BaseResponse<bool>()
                {
                    Data = user != null,
                    Description = user != null ? "Логин уже занят" : "Логин свободен",
                    StatusCode = user != null ? HttpStatusCode.UnprocessableEntity : HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Registration(RegistrationViewModel registrationViewModel)
        {
            try
            {
                var user = new User()
                {
                    Login = registrationViewModel.Login,
                    Password = HashPasswordHelper.GetHashPassword(registrationViewModel.Password)
                };

                await _userRepository.Create(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = Authenticate(user),
                    Description = "Аккаунт создан",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _userRepository.GetByLogin(loginViewModel.Login);
                if (user?.Password == HashPasswordHelper.GetHashPassword(loginViewModel.Password))
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = Authenticate(user),
                        Description = "Вход успешно выполнен",
                        StatusCode = HttpStatusCode.OK
                    };

                return new BaseResponse<ClaimsIdentity>()
                {
                        Description = "Неправильный логин или пароль",
                        StatusCode = HttpStatusCode.UnprocessableEntity
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        static private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            return new ClaimsIdentity(claims, authenticationType: "ApplicationCookie",
                ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}




