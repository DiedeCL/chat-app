using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public class MessageService
    {
        private ChatAppContext _context;

        public MessageService(ChatAppContext context)
        {
            _context = context;
        }
        public bool SendMessage(string message, string emailReceiver, User sender, int conversationId)
        {
            try
            {
                var aesKey = EncryptionServices.GetAesKey();
                var aesIv = EncryptionServices.GetAesIV();
                var publicKeyReceiver = _context.PublicKeyStores.FirstOrDefault(s => s.Email == emailReceiver)?.PublicKey;
                
                var encryptedMessageWithAes = EncryptionServices.EncryptWithAes(aesKey, aesIv, message);
                var encryptedAesKeyWithRsa = EncryptionServices.EncryptWithRsa(aesKey, publicKeyReceiver);
                var encryptedAesIVWithRsa =aesIv;
                
                var conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
                if (conversation == null) return false;
                var message1 = new Message
                {
                    EmailOfSender = sender.Email,
                    ConversationId = conversationId,
                    EncryptedAesIV = encryptedAesIVWithRsa,
                    EncryptedAesKey = encryptedAesKeyWithRsa,
                    EncryptedContentOfMessage = encryptedMessageWithAes, 
                    SignedData = EncryptionServices.SingData(message, sender.PrivateKey)
                };

                conversation.Messages.Add(message1);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
           
        }

      
        public bool CreateNewConversation(User sender, User receiver)
        {
            try
            {
                var userConversationSender = new UserConversation();
                var userConversationReceiver = new UserConversation();
                var userConversations = new List<UserConversation>();
                userConversations.Add(userConversationSender);
                userConversations.Add(userConversationReceiver);


                var user = _context.Users.FirstOrDefault(u => u.Id == sender.Id);
                user?.UserConversations.Add(userConversationSender);
                _context.Users.FirstOrDefault(u => u.Id == receiver.Id)?.UserConversations.Add(userConversationReceiver);
                _context.Conversations.Add(new Conversation { UserConversations = userConversations }
                );
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
            
        }

      

        public Conversation GetConversationById(int id)
        {
            return _context.Conversations.FirstOrDefault(c => c.Id == id);
        }
    }
}
