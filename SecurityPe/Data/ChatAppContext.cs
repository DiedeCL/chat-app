using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<UserKey> UserKeys { get; set; }

        public ChatAppContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<Conversation>().HasKey(conversation1 => conversation1.Id);
            modelBuilder.Entity<Message>().HasKey(message => message.Id);

            // create foreign key of Users 

            /*
            modelBuilder.Entity<UserConversation>()
                .HasKey(conversation => new { conversation.UserId, conversation.ConversationId });
            modelBuilder.Entity<UserConversation>()
                .HasOne(c => c.Conversation)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(c => c.ConversationId);

            modelBuilder.Entity<UserConversation>()
                .HasOne(c => c.User)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(c => c.UserId);
                */

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(message => message.ConversationId);
           

            base.OnModelCreating(modelBuilder);
        }
    }
}