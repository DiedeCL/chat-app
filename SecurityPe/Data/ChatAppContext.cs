using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecurityPe.Domain;

namespace SecurityPe.Data
{
    public class ChatAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";   // update me
            builder.UserID = "sa";              // update me
            builder.Password = "your_password";      // update me
            builder.InitialCatalog = "EFSampleDB";
            */

            var connectionString =
                @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChatAppDB;Integrated Security=True\";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set all fields of user as required 
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            
            //create primaryKey of Messages 
            modelBuilder.Entity<Message>().HasKey(message => message.Id);
            
            
            // create foreign key of Users 
            modelBuilder.Entity<Conversation>().Property(c => c.Id).IsRequired();
            modelBuilder.Entity<UserConversation>()
                .HasKey(conversation => new {conversation.UserId, conversation.ConversationId});
            modelBuilder.Entity<UserConversation>()
                .HasOne(c => c.Conversation)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(c => c.ConversationId);   
            
            modelBuilder.Entity<UserConversation>()
                .HasOne(c => c.User)
                .WithMany(u => u.UserConversations)
                .HasForeignKey(c => c.UserId);



            /*Create Data*/ 
            //create Users
            var bogaert = new User()
            {
                Id = 1,
                Name = "Bogaert",
                Email = "Bogaert@gmail.com",
                Password = "12345",
                
            };
            var boris = new User
            {
                Id = 2,
                Name = "Boris",
                Email = "boris@gmail.com",
                Password = "12345",
               
            };
            //create conversation
            var conversation = new Conversation
            {
                Id = 0,
            };
            // create Message
            var message1 = new Message()
            {
                ContentOfMessage = "Hello Bogaert",
                Id = 1,
                IdOfSender = boris.Id
            };
            var message2 = new Message()
            {
                ContentOfMessage = "Hello Boris",
                Id = 2,
                IdOfSender = bogaert.Id
            };
            var message3 = new Message()
            {
                ContentOfMessage = "Are you ok?",
                Id = 3,
                IdOfSender = boris.Id
            };

            // create the messages
            var messages = new List<Message>{message1, message2,message3};
            
            // add messages to conversation
            conversation.Messages = messages;

            // create userConversation
            var userConversationOfBogaert = new UserConversation
            {
                User = bogaert,
                UserId = bogaert.Id,
                Conversation = conversation,
                ConversationId = conversation.Id
            }; 
            var userConversationOfBoris = new UserConversation
            {
                User = boris,
                UserId = boris.Id,
                Conversation = conversation,
                ConversationId = conversation.Id
            };
            
            // add userConverstaions to users 
            bogaert.UserConversations = new Collection<UserConversation>{userConversationOfBogaert};
            boris.UserConversations = new Collection<UserConversation>{userConversationOfBoris};
            
            // Add data to db
            modelBuilder.Entity<Conversation>().HasData(conversation);
            modelBuilder.Entity<User>().HasData(boris, bogaert);
            modelBuilder.Entity<UserConversation>().HasData(userConversationOfBogaert, userConversationOfBoris);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}