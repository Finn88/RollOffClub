using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<OranizationContact> OranizationContacts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
