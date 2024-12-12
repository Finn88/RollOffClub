
namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordSalt { get; set; }
        public Role[] Roles { get; set; } = [];
    }
}
