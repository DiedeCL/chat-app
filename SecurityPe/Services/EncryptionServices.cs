using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SecurityPe.Domain;


namespace SecurityPe.Services
{
    public class EncryptionServices
    {


        public static string GetPublicKey()
        {
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            return rsaCryptoService.ToXmlString(false);
        }

        public static string GetPrivateKey()
        {
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            return rsaCryptoService.ToXmlString(true);
        }

        public static string EncryptWithRsa(byte[] data, string key)
        {
            using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
            rsaCryptoService.FromXmlString(key);
            return Encoding.ASCII.GetString(rsaCryptoService.Encrypt(data, RSAEncryptionPadding.OaepSHA256));
        }

        public static string DecryptWithRsa(byte[] data, string key)
        {
            using (RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider())
            {
                rsaCryptoService.FromXmlString(key);
                return Encoding.ASCII.GetString(rsaCryptoService.Decrypt(data, RSAEncryptionPadding.OaepSHA256));
            }
        }

        public static byte[] GetAesKey()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateKey();
                return provider.Key;
            }
        }

        public static byte[] GetAesIV()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateIV();
                return provider.IV;
            }
        }
        public static string EncryptWithAes(byte[] key, byte[] IV, string plainText)
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
                provider.Key = key;
                provider.IV = IV;

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
            return Encoding.ASCII.GetString(encrypted);
        }


        static string DecryptWithAes(byte[] cipherText, byte[] key, byte[] IV)
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


        public static string CreateHashFromString(string message)
        {
            using MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            var computeHash = provider.ComputeHash(Encoding.ASCII.GetBytes(message));
            var md5Hash = new StringBuilder();

            foreach (var hashByte in computeHash)
            {
                md5Hash.Append(hashByte);
            }

            return md5Hash.ToString();
        }
    }
}



