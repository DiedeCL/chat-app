using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
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
                try
                {
                    rsaCryptoService.FromXmlString(key);
                    return rsaCryptoService.Decrypt(Convert.FromBase64String(data), true);
                }
                catch (Exception e)
                {
                    
                    return new byte[0];
                }
               
               
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
                return String.Empty;
            if (key == null || key.Length <= 0)
                return String.Empty;
            if (IV == null || IV.Length <= 0)
                return String.Empty;

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

        //  Call this function to remove the key from memory after use for security
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        /// <summary>
        /// Creates a random salt that will be used to encrypt your file. This method is required on FileEncrypt.
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        
        // Encrypts a file from its path and a plain password.
        private void FileEncrypt(string inputFile, string password)
        {
            //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cryptoStream = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                   
                    cryptoStream.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cryptoStream.Close();
                fsCrypt.Close();
            }
        }

        
        // Decrypts an encrypted file with the FileEncrypt method through its path and the plain password.
        private void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            FileStream encryptedFileStream = new FileStream(inputFile, FileMode.Open);
            encryptedFileStream.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(encryptedFileStream, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                   
                   fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                encryptedFileStream.Close();
            }
        }
    }
}