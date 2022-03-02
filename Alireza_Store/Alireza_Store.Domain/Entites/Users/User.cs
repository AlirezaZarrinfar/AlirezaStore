using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alireza_Store.Domain.Entites.Users
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; }

    }
}
