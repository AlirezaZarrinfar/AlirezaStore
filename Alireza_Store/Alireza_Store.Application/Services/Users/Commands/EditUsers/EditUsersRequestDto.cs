namespace Alireza_Store.Application.Services.Users.Commands.EditUsers
{
    public class EditUsersRequestDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public bool IsCheckedPass { get; set; }
    }
}
