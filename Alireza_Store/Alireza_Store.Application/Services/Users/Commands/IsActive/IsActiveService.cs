using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;

namespace Alireza_Store.Application.Services.Users.Commands.IsActive
{
    public class IsActiveService : IIsActiveService
    {
        private readonly IDatabaseContext _context;
        public IsActiveService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long Id)
        {
            var user = _context.Users.Find(Id);
            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            if (user.IsActive)
            {
                return new ResultDto { IsSuccess = true, Message = "با موفقیت فعال شد" };
            }
            else
            {
                return new ResultDto { IsSuccess = true, Message = "با موفقیت غیر فعال شد" };
            }
        }
    }
}
