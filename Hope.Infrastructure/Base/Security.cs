using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;

namespace Hope.Infrastructure.Base
{
    public class Security
    {
        public string EncryptString(string message)
        {
            Byte[] Results;
            System.Text.UTF8Encoding UTF8 = new UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDEKey = HashProvider.ComputeHash(UTF8.GetBytes("P@ssw0rd"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key= TDEKey;
            TDESAlgorithm.Mode= CipherMode.ECB;
            TDESAlgorithm.Padding= PaddingMode.PKCS7;
            byte[] DataToEncryptor=UTF8.GetBytes(message);
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results=Encryptor.TransformFinalBlock(DataToEncryptor,0,DataToEncryptor.Length);
            var d = Convert.ToBase64String(Results);
            return d;
        }
        public string DecryptString(string message)
        {
            Byte[] Results;
            System.Text.UTF8Encoding UTF8 = new UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDEKey = HashProvider.ComputeHash(UTF8.GetBytes("P@ssw0rd"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDEKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            string urlDecede = HttpUtility.UrlDecode(message);
            Byte[] DataToEncrypt;
            try
            {
                if (urlDecede.Length >= 12)
                {
                    DataToEncrypt = Convert.FromBase64String(message);
                }
                else
                {
                    return message;
                }
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            catch (Exception )
            {
                return message; 
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            var d = UTF8.GetString(Results);
            return d;
        }
    }
}
