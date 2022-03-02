namespace Alireza_Store.Application.Services.Users.Commands.AddUsers
{
    public class AddUsersRequestDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public long RoleId { get; set; }
    }
}
