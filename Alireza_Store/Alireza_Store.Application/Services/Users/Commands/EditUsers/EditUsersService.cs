using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Text.RegularExpressions;

namespace Alireza_Store.Application.Services.Users.Commands.EditUsers
{
    public class EditUsersService : IEditUsersService
    {
        private readonly IDatabaseContext _context;
        private readonly IPasswordHasher _hasher;

        public EditUsersService(IDatabaseContext context, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _context = context;
        }
        public ResultDto Execute(long Id, EditUsersRequestDto request)
        {
            try
            {
                var user = _context.Users.Find(Id);
                if (user == null)
                {
                    return new ResultDto { Message = "کاربر یافت نشد", IsSuccess = false };
                }
                if (string.IsNullOrEmpty(request.Name))
                {
                    return new ResultDto { Message = "لطفا نام را وارد کنید", IsSuccess = false };
                }
                if (string.IsNullOrEmpty(request.LastName))
                {
                    return new ResultDto { Message = "لطفا نام خانوداگی را وارد کنید", IsSuccess = false };
                }
                if (string.IsNullOrEmpty(request.Email))
                {
                    return new ResultDto { Message = "لطفا ایمیل را وارد کنید", IsSuccess = false };
                }
                string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto { IsSuccess = false, Message = "ایمیل خود را به درستی وارد نمایید" };
                }
                if (request.IsCheckedPass)
                {
                    if (string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.OldPassword))
                    {
                        return new ResultDto { Message = "لطفا رمز عبور را وارد کنید", IsSuccess = false };
                    }
                    if (_hasher.VerifyHashedPassword(user.Password, request.OldPassword).ToString() == "Failed")
                    {
                        return new ResultDto { IsSuccess = false, Message = "رمز عبور قدیمی شما درست نیست" };
                    }
                    user.Name = request.Name;
                    user.LastName = request.LastName;
                    user.RoleId = request.RoleId;
                    user.Email = request.Email;
                    user.Password = _hasher.HashPassword(request.NewPassword);
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true, Message = "انجام شد" };
                }
                else
                {
                    user.Name = request.Name;
                    user.LastName = request.LastName;
                    user.RoleId = request.RoleId;
                    user.Email = request.Email;
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true, Message = "انجام شد" };
                }
            }
            catch (Exception)
            {
                return new ResultDto { Message = "مشکلی پیش آمذه است", IsSuccess = false };
            }
        }
        

    }
}
