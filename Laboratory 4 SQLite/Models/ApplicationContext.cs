using System.Data.Entity;

namespace Laboratory_4_SQLite.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Material> Materials { get; set; }
    }
}
