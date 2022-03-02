using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Services.Users.Login
{
    public interface IUserLoginService
    {
        public ResultDto Execute(string Email, string Password);
        public ClaimsPrincipal principal();
        public AuthenticationProperties properties();
    }
    
    public class UserLoginService : IUserLoginService
    {
        public ClaimsPrincipal principal ;
        public AuthenticationProperties properties;


        private readonly IDatabaseContext _context;
        private readonly IPasswordHasher _hasher;

        public UserLoginService(IDatabaseContext context, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _context = context;
        }
        public ResultDto Execute(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return new ResultDto { IsSuccess = false, Message = "لطفا ایمیل را وارد کنید", UserId = 0 };
            }
            var user = _context.Users.First(p => p.Email == Email);
            if (string.IsNullOrEmpty(Password))
            {
                return new ResultDto { IsSuccess = false, Message = "لطفا رمز عبور را وارد کنید", UserId = 0 };
            }
            if (user.IsActive == false || user.IsDeleted == true || user == null)
            {
                return new ResultDto { IsSuccess = false, Message = "کاربر یافت نشد", UserId = 0 };
            }
            if (_hasher.VerifyHashedPassword(user.Password, Password).ToString() == "Failed")
            {
                return new ResultDto { IsSuccess = false, Message = "رمز عبور شما درست نیست", UserId = 0 };
            }
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name , user.Name),
                    new Claim(ClaimTypes.Surname , user.LastName),
                    new Claim(ClaimTypes.Role, "Customer"),
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            principal = new ClaimsPrincipal(identity);
            properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };
            return new ResultDto { IsSuccess = true, Message = "با موفقیت وارد شدید", UserId = user.Id };
        }

        ClaimsPrincipal IUserLoginService.principal()
        {
            return principal;
        }

        AuthenticationProperties IUserLoginService.properties()
        {
            return properties;
        }
    }
}
