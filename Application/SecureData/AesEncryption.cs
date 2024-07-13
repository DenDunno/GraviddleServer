using System.Security.Cryptography;
using System.Text;

namespace Application.SecureData;

public class AesEncryption : IEncryption
{
    public string Encrypt(string plainText, string key)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.GenerateIV();

        using MemoryStream memoryStream = new();
        memoryStream.Write(aes.IV, 0, aes.IV.Length);
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string cipherText, string key)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        using MemoryStream memoryStream = new(cipherTextBytes);
        byte[] iv = new byte[aes.BlockSize / 8];
        memoryStream.Read(iv, 0, iv.Length);
        aes.IV = iv;
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);
        return streamReader.ReadToEnd();
    }
}