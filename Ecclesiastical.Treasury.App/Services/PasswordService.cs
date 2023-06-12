using System.Security.Cryptography;
using System.Text;
using Ecclesiastical.Treasury.App.Services.Interfaces;

namespace Ecclesiastical.Treasury.App.Services;

public class PasswordService : IPasswordService
{
    public string GetHash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedPassword = SHA512.HashData(passwordBytes);
        var base64Hash = Convert.ToBase64String(hashedPassword);
        return base64Hash;
    }

    public bool ValidatePassword(string entered, string stored)
    {
        var hashedPassword = this.GetHash(entered);
        return hashedPassword == stored;
    }
}