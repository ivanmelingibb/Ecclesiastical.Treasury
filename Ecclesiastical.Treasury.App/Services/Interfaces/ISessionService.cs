using Ecclesiastical.Treasury.App.Data;
using Ecclesiastical.Treasury.App.Models;

namespace Ecclesiastical.Treasury.App.Services.Interfaces;

public interface ISessionService
{
    User? User { get; set; }
    Task<Result> RegisterAsync(string username, string password);
    Task<Result> LoginAsync(string username, string password);
    Task LogoutAsync();
}