
namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required bool IsAdministrator { get; set; }
    }
}
