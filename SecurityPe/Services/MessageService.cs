using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public class MessageService
    {
        public bool SendMessage(string message,string publicKeyReceiver, User Sender)
        {
            var aesKey = EncryptionServices.GetAesKey();
            var aesIv = EncryptionServices.GetAesIV();

            var encryptedMessageWithAes = EncryptionServices.EncryptWithAes(aesKey, aesIv, message);
            var encryptedAesKeyWithRsa = EncryptionServices.EncryptWithRsa(aesKey,publicKeyReceiver);
            var encryptedAesIVWithRsa = EncryptionServices.EncryptWithRsa(aesIv, publicKeyReceiver);

            var hashFromMessage = EncryptionServices.CreateHashFromString(message);

            return true;
        }

    }
}
