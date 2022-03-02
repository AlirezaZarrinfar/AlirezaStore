using Alireza_Store.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Services.Users.Commands.DeleteUsers
{
    public interface IDeleteUserSerivce
    {
       public ResultDto Execute(long Id);
    }
}
