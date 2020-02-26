using Microsoft.EntityFrameworkCore;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class ChatAppContext : DbContext
    {
        private DbSet<User> Users { get; set; }
        private DbSet<Conversation> Conversations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
                @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChatAppDB;Integrated Security=True\";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            
            modelBuilder.Entity<Conversation>().Property(c => c.UserIds).IsRequired();
            modelBuilder.Entity<Conversation>().Property(c => c.Id).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}