using Alireza_Store.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        public ResultDto<List<GetUsersResultDto>> Execute(GetUsersRequestDto requests);
    }
}
