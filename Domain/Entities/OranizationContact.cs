
namespace Domain.Entities
{
    public class OranizationContact
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public required string Value { get; set; }
    }
}
