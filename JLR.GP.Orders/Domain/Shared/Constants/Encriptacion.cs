using System.Security.Cryptography;
using System.Text;

namespace Api_Dc.Domain.Shared.Constants
{
    public class Encriptacion
    {
        // Ejemplo de clave de 256 bits (32 caracteres)
        private static readonly string EncryptionKey = "12345678901234567890123456789012";

        // Ejemplo de IV de 128 bits (16 caracteres)
        private static readonly string EncryptionIV = "1234567890123456";
        public static string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Debe ser de 32 bytes para AES-256
                aesAlg.IV = Encoding.UTF8.GetBytes(EncryptionIV); // Debe ser de 16 bytes

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Debe ser de 32 bytes para AES-256
                aesAlg.IV = Encoding.UTF8.GetBytes(EncryptionIV); // Debe ser de 16 bytes

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            }catch(Exception ex)
            {
                return encryptedPassword;
            }
        }
    }
}
