using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Application.Interfaces.FacadPattern;
using Alireza_Store.Application.Services.Users.Commands.AddUsers;
using Alireza_Store.Application.Services.Users.Commands.DeleteUsers;
using Alireza_Store.Application.Services.Users.Commands.EditUsers;
using Alireza_Store.Application.Services.Users.Commands.IsActive;
using Alireza_Store.Application.Services.Users.Login;
using Alireza_Store.Application.Services.Users.Queries.GetRoles;
using Alireza_Store.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Services.Users.FacadPattern
{
    public class UsersFacad : IUsersFacad
    {
        private readonly IDatabaseContext _context;
        private readonly IPasswordHasher _hasher;
        public UsersFacad(IDatabaseContext context, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _context = context;
        }
        private IGetUsersService _getUsersService;
        public IGetUsersService getUsersService
        {
            get
            {
                return _getUsersService = _getUsersService ?? new GetUsersService(_context);
            }
        }

        private IGetRolesService _getRolesService;
        public IGetRolesService getRolesService
        {
            get
            {
                return _getRolesService = _getRolesService ?? new GetRolesService(_context);
            }
        }

        private IAddUsersService _addUsersService;
        public IAddUsersService addUsersService
        {
            get
            {
                return _addUsersService = _addUsersService ?? new AddUsersService(_context, _hasher);
            }
        }

        private IEditUsersService _editUsersService;
        public IEditUsersService editUsersService
        {
            get
            {
                return _editUsersService = _editUsersService ?? new EditUsersService(_context, _hasher);
            }
        }

        private IDeleteUserSerivce _deleteUserSerivce;
        public IDeleteUserSerivce deleteUserSerivce
        {
            get
            {
                return _deleteUserSerivce = _deleteUserSerivce ?? new DeleteUserService(_context);
            }
        }

        private IIsActiveService _isActiveService;
        public IIsActiveService isActiveService
        {
            get
            {
                return _isActiveService = _isActiveService ?? new IsActiveService(_context);
            }
        }
        private IUserLoginService _userLoginService;
        public IUserLoginService userLoginService  
        { 
            get
            {
                return _userLoginService = _userLoginService ?? new UserLoginService(_context,_hasher);
            }
        }
    }
}
