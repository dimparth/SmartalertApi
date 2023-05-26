using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _iv = new byte[]
        {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        private readonly byte[] _key = new byte[]
        {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        public string Decrypt(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new Exception("password is null");
            }
            byte[] bytes = Convert.FromBase64String(input.Replace(" ", "+"));
            using Aes aes = Aes.Create();
            MemoryStream memoryStream = new();
            CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            var decrypted = Encoding.UTF8.GetString(memoryStream.ToArray());
            return decrypted;
        }

        public string Encrypt(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new Exception("password is null");
            }
            using Aes aes = Aes.Create();
            MemoryStream memoryStream = new();
            CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
            StreamWriter writer = new(cryptoStream);
            writer.Write(input);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            var cryptographed = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            return cryptographed;
        }
    }
}
