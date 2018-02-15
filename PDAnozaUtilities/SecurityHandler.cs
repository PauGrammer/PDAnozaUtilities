using System;
using System.Text;
using System.Security.Cryptography;

namespace PDAnozaUtilities
{
    public class SecurityHandler
    {
        private string GetHashKey()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            var month = DateTime.Now.Month.ToString();

            return "Pau" + dayOfWeek + month + "Anoza";
        }

        public string EncryptString(string decryptedText)
        {
            var desCryptoService = new TripleDESCryptoServiceProvider();
            var md5CryptoService = new MD5CryptoServiceProvider();
            desCryptoService.Key = md5CryptoService.ComputeHash(ASCIIEncoding.ASCII.GetBytes(GetHashKey()));
            desCryptoService.Mode = CipherMode.ECB;
            var iCryptoTransform = desCryptoService.CreateEncryptor();
            var byteEquivalent = UTF8Encoding.UTF8.GetBytes(decryptedText);
            return Convert.ToBase64String(iCryptoTransform.TransformFinalBlock(byteEquivalent, 0, byteEquivalent.Length));
        }

        public string EncryptString(string decryptedText, string hashKey)
        {
            var desCryptoService = new TripleDESCryptoServiceProvider();
            var md5CryptoService = new MD5CryptoServiceProvider();
            desCryptoService.Key = md5CryptoService.ComputeHash(ASCIIEncoding.ASCII.GetBytes(hashKey));
            desCryptoService.Mode = CipherMode.ECB;
            var iCryptoTransform = desCryptoService.CreateEncryptor();
            var byteEquivalent = UTF8Encoding.UTF8.GetBytes(decryptedText);
            return Convert.ToBase64String(iCryptoTransform.TransformFinalBlock(byteEquivalent, 0, byteEquivalent.Length));
        }

        public string DecryptString(string encryptedText)
        {
            encryptedText = encryptedText.Replace(" ", "+");
            var desCryptoService = new TripleDESCryptoServiceProvider();
            var md5CryptoService = new MD5CryptoServiceProvider();
            desCryptoService.Key = md5CryptoService.ComputeHash(ASCIIEncoding.ASCII.GetBytes(GetHashKey()));
            desCryptoService.Mode = CipherMode.ECB;
            var iCryptoTransform = desCryptoService.CreateDecryptor();
            var byteEquivalent = Convert.FromBase64String(encryptedText);
            string decryptedText;

            try
            {
                decryptedText = UTF8Encoding.UTF8.GetString(iCryptoTransform.TransformFinalBlock(byteEquivalent, 0, byteEquivalent.Length));
            }
            catch (Exception)
            {
                decryptedText = "Invalid";
            }
            return decryptedText;
        }

        public string DecryptString(string encryptedText, string hashKey)
        {
            encryptedText = encryptedText.Replace(" ", "+");
            var desCryptoService = new TripleDESCryptoServiceProvider();
            var md5CryptoService = new MD5CryptoServiceProvider();
            desCryptoService.Key = md5CryptoService.ComputeHash(ASCIIEncoding.ASCII.GetBytes(hashKey));
            desCryptoService.Mode = CipherMode.ECB;
            var iCryptoTransform = desCryptoService.CreateDecryptor();
            var byteEquivalent = Convert.FromBase64String(encryptedText);
            return UTF8Encoding.UTF8.GetString(iCryptoTransform.TransformFinalBlock(byteEquivalent, 0, byteEquivalent.Length));
        }
    }
}
