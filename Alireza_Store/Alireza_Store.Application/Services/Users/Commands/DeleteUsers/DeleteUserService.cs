using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Alireza_Store.Application.Services.Users.Commands.DeleteUsers
{
    public class DeleteUserService : IDeleteUserSerivce
    {
    private readonly IDatabaseContext _context;
        public DeleteUserService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long Id)
        {
            try
            {
                var user = _context.Users.Find(Id);
                if(user == null)
                {
                    return new ResultDto { IsSuccess = false, Message = "کاربر یافت نشد" };
                }
                user.IsDeleted = true;
                user.DeleteDate = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "با موفقیت حذف شد" };
            }
            catch (Exception)
            {
                return new ResultDto { IsSuccess = false, Message = "مشکلی وجود دارد" };
            }
        }
    }
   
}


