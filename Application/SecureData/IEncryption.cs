namespace Application.SecureData;

public interface IEncryption
{
    string Encrypt(string plainText, string key);
    string Decrypt(string cipherText, string key);
}