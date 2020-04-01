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
            throw new NotImplementedException();
            
        }

        public string GetPrivateKey(int userId)
        {
            throw new NotImplementedException();

        }


    }



}


