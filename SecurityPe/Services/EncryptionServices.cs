using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SecurityPe.Services
{
    public class EncryptionServices
    {
        private RSACryptoServiceProvider _rsaCryptoService = new RSACryptoServiceProvider();

        public string GetPublicKey()
        {
            return _rsaCryptoService.ToXmlString(false);

        }

        public string GetPrivateKey()
        {
           return _rsaCryptoService.ToXmlString(true);
            
        }

        public static string EncryptWithRsa(string data, string key)
        {
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            rsaCryptoService.FromXmlString(key);
            return Convert.ToBase64String(rsaCryptoService.Encrypt(Convert.FromBase64String(data), true));
            
        }

        public static byte[] DecryptWithRsa(string data, string key)
        {
            
            using (RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider())
            {

                rsaCryptoService.FromXmlString(key);
                return rsaCryptoService.Decrypt(Convert.FromBase64String(data), true);
               
            }
        }

        public static string GetAesKey()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateKey();
                return Convert.ToBase64String(provider.Key);
            }
        }

        public static string GetAesIV()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateIV();
                return Convert.ToBase64String(provider.IV);
            }
        }

        public static string EncryptWithAes(string key, string IV, string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = Convert.FromBase64String(key);
                provider.IV = Convert.FromBase64String(IV);
                provider.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = provider.CreateEncryptor(provider.Key, provider.IV);

                // Create the streams used for encryption.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                        new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cryptoStream))
                        {
                            //Write all data to the stream.
                            writer.Write(plainText);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptWithAes(byte[] cipherText, byte[] key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = key;
                provider.IV = IV;
                provider.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = provider.CreateDecryptor(provider.Key, provider.IV);

                // Create the streams used for decryption.
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = reader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static string SingData(string message, string key)
        {
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            rsaCryptoService.FromXmlString(key);
            var buffer = Encoding.Unicode.GetBytes(message);
            var singedData = rsaCryptoService.SignData(buffer, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            
;            return Convert.ToBase64String(singedData);
        }   
        
        
        public static bool VerifyData(string message, string key, string signedData)
        {
            
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            rsaCryptoService.FromXmlString(key);
            var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            var buffer = Encoding.Unicode.GetBytes(message);
            return rsaCryptoService.VerifyData(buffer, Convert.FromBase64String(signedData), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);




        }
    }
}