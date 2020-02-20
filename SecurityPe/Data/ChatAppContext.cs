using Microsoft.EntityFrameworkCore;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class ChatAppContext : DbContext
    {
        private DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}