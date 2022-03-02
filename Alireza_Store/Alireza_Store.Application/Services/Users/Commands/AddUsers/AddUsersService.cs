using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;
using Alireza_Store.Domain.Entites.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Alireza_Store.Application.Services.Users.Commands.AddUsers
{
    public class AddUsersService : IAddUsersService
    {
        private readonly IDatabaseContext _context;
        private readonly IPasswordHasher _hasher;
        public AddUsersService(IDatabaseContext context, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _context = context;
        }
        ResultDto IAddUsersService.Execute(AddUsersRequestDto requests)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(requests.Name))
                {
                    return new ResultDto { IsSuccess = false, Message = "لطفا نام را وارد کنید" };
                }
                if (string.IsNullOrWhiteSpace(requests.LastName))
                {
                    return new ResultDto { IsSuccess = false, Message = "لطفا نام خانوادگی را وارد کنید" };
                }
                if (string.IsNullOrWhiteSpace(requests.Email))
                {
                    return new ResultDto { IsSuccess = false, Message = "لطفا ایمیل را وارد کنید" };
                }
                string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
                var match = Regex.Match(requests.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto { IsSuccess = false, Message = "ایمیل خود را به درستی وارد نمایید" };
                }
                if (string.IsNullOrWhiteSpace(requests.Password) || string.IsNullOrWhiteSpace(requests.RePassword))
                {
                    return new ResultDto { IsSuccess = false, Message = "لطفا رمزعبور را وارد کنید" };
                }
                if (requests.Password != requests.RePassword)
                {
                    return new ResultDto { IsSuccess = false, Message = "رمزعبور و تکرار آن یکسان نیست" };
                }
                string Hashed = _hasher.HashPassword(requests.Password);
                User user = new User()
                {
                    Name = requests.Name,
                    LastName = requests.LastName,
                    Email = requests.Email,
                    RoleId = requests.RoleId,
                    IsActive = true,
                    Password = Hashed,
                    IsDeleted = false,
                };
                long id = user.Id;
                _context.Users.Add(user);
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "عملیات با موفقیت انجام شد",UserId = user.Id };
            }
            catch (Exception)
            {
                return new ResultDto { IsSuccess = false, Message = "عملیات با موفقیت انجام نشد" };
            }
        }
    }
}
