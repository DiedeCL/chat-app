using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class ChatAppContext : IdentityDbContext<User, Role, int>
    {
       
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PublicKeyStore> PublicKeyStores { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<StoredFile> StoredFiles { get; set; }

        public ChatAppContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Conversation>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(message => message.ConversationId);
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.UserConversations)
                .WithOne(u => u.Conversation);
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserConversations)
                .WithOne(uc => uc.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}