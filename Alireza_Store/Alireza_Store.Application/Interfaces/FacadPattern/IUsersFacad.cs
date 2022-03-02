using Alireza_Store.Application.Services.Users.Commands.AddUsers;
using Alireza_Store.Application.Services.Users.Commands.DeleteUsers;
using Alireza_Store.Application.Services.Users.Commands.EditUsers;
using Alireza_Store.Application.Services.Users.Commands.IsActive;
using Alireza_Store.Application.Services.Users.Login;
using Alireza_Store.Application.Services.Users.Queries.GetRoles;
using Alireza_Store.Application.Services.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Interfaces.FacadPattern
{
    public interface IUsersFacad
    {
       public IGetUsersService getUsersService { get; }
       public IGetRolesService getRolesService { get; }
       public IAddUsersService addUsersService { get; }
       public IEditUsersService editUsersService { get; }
       public IDeleteUserSerivce deleteUserSerivce { get; }
       public IIsActiveService isActiveService { get; }
       public IUserLoginService userLoginService { get; }
    }

}
