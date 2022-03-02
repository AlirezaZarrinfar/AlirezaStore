using Alireza_Store.Application.Interfaces.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Alireza_Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDatabaseContext _context;
        public GetRolesService(IDatabaseContext context)
        {
            _context = context;
        }
        public List<GetRolesDto> Execute()
        {
            var Roles = _context.Roles.Select(p => new GetRolesDto { Id = p.Id, Name = p.Name }).ToList();
            return Roles;
        }
    }



}
