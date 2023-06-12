namespace Ecclesiastical.Treasury.App.Services.Interfaces;

public interface IPasswordService
{
    string GetHash(string password);
    bool ValidatePassword(string entered, string stored);
}