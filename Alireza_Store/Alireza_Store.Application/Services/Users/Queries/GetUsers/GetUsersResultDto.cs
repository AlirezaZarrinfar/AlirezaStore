namespace Alireza_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersResultDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
    }
}
