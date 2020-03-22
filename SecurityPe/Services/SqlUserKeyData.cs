using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public class SqlUserKeyData
    {
        private readonly ChatAppContext _context;

        public SqlUserKeyData(ChatAppContext context)
        {
            _context = context;
        }

        public string GetPublicKey(int userId)
        {
            return GetUserKey(userId).PublicKey;
        }

        public string GetPrivateKey(int userId)
        {
            return GetUserKey(userId).PrivateKey;

        }

        private UserKey GetUserKey(int userId)
        {
            return _context.UserKeys.FirstOrDefault(uk => uk.UserId == userId);
        }

        public UserKey CreateUserKey()
        {

            return new UserKey
            {
                PublicKey = EncryptionServices.GetPublicKey(),
                PrivateKey = EncryptionServices.GetPrivateKey()
            };
        }

    }



}


