using Alireza_Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Services.Users.Commands.AddUsers
{
    public interface IAddUsersService
    {
        public ResultDto Execute(AddUsersRequestDto requests);
    }
}
