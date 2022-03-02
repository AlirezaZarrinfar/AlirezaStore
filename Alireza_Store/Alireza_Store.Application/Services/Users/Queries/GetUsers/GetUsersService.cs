using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Common;
using System.Collections.Generic;
using System.Linq;

namespace Alireza_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDatabaseContext _context;
        public GetUsersService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetUsersResultDto>> Execute(GetUsersRequestDto requests)
        {
                var Users = _context.Users.AsQueryable();
                Users = Users.Where(p => p.IsDeleted == false);
                if (!string.IsNullOrEmpty(requests.SearchKey))
                {
                    Users = Users.Where(p => p.Name.Contains(requests.SearchKey)
                    || p.LastName.Contains(requests.SearchKey)
                    || p.Email.Contains(requests.SearchKey));
                }
                var user = Users.Select(p => new GetUsersResultDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LastName = p.LastName,
                    IsActive = p.IsActive,
                    RoleId = p.RoleId,
                    Email = p.Email,
                    Role = p.Role.Name,
                }).ToList();
                return new ResultDto<List<GetUsersResultDto>>
                {
                    IsSuccess = true,
                    Data = user
                };
            }
        }
    }

