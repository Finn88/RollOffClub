
namespace Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public OranizationContact[] Contacts { get; set; } = [];
        public User[] User { get; set; } = [];
    }
}
