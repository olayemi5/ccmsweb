using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ICAD.Core.Manager
{
   public class Encryption
    {
       public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
       {
           byte[] encryptedBytes = null;

           // Set your salt here, change it to meet your flavor:
           // The salt bytes must be at least 8 bytes.
           byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

           using (MemoryStream ms = new MemoryStream())
           {
               using (RijndaelManaged AES = new RijndaelManaged())
               {
                   AES.KeySize = 256;
                   AES.BlockSize = 128;

                   var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                   AES.Key = key.GetBytes(AES.KeySize / 8);
                   AES.IV = key.GetBytes(AES.BlockSize / 8);

                   AES.Mode = CipherMode.CBC;

                   using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                   {
                       cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                       cs.Close();
                   }
                   encryptedBytes = ms.ToArray();
               }
           }

           return encryptedBytes;
       }


       public static string EncryptText(string input, string password)
       {
           // Get the bytes of the string
           byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
           byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
           
           
           // Hash the password with SHA256
           passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

           byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

           string result = Convert.ToBase64String(bytesEncrypted);

           return result;
       }


       public static string EncryptText(string input)
       {
           // Get the bytes of the string
           byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
          // byte[] passwordBytes = Encoding.UTF8.GetBytes(password);


           // Hash the password with SHA256
           bytesToBeEncrypted = SHA256.Create().ComputeHash(bytesToBeEncrypted);

          //byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

           string result = Convert.ToBase64String(bytesToBeEncrypted);

           return result;
           //return bytesToBeEncrypted.ToString();
       }


       public static string Base64Encode(string plainText)
       {
           var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
           return System.Convert.ToBase64String(plainTextBytes);
       }

       public static string GenerateSHA256String(string inputString)
       {
           SHA256 sha256 = SHA256Managed.Create();
           byte[] bytes = Encoding.UTF8.GetBytes(inputString);
           byte[] hash = sha256.ComputeHash(bytes);
           return GetStringFromHash(hash);
       }

       public static string GenerateSHA512String(string inputString)
       {
           SHA512 sha512 = SHA512Managed.Create();
           byte[] bytes = Encoding.UTF8.GetBytes(inputString);
           byte[] hash = sha512.ComputeHash(bytes);
           return GetStringFromHash(hash);
       }

       private static string GetStringFromHash(byte[] hash)
       {
           StringBuilder result = new StringBuilder();
           for (int i = 0; i < hash.Length; i++)
           {
               result.Append(hash[i].ToString("X2"));
           }
           return result.ToString();
       }
    }
}
