using System.Security.Claims;
using Ecclesiastical.Treasury.App.Data;
using Ecclesiastical.Treasury.App.Models;
using Ecclesiastical.Treasury.App.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Ecclesiastical.Treasury.App.Services;

public class SessionService : ISessionService
{
    private static string Cookie => CookieAuthenticationDefaults.AuthenticationScheme;
    
    private readonly IPasswordService _passwordService;
    private readonly EcclesiasticalDbContext _dbContext;
    private readonly HttpContext _httpContext;

    public SessionService(IPasswordService passwordService, EcclesiasticalDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this._passwordService = passwordService;
        this._dbContext = dbContext;
        this._httpContext = httpContextAccessor.HttpContext!;
    }

    public User? User { get; set; }

    public async Task<Result> RegisterAsync(string username, string password)
    {
        if (await this._dbContext.Users.AnyAsync(u => u.Username == username))
        {
            return new("This user is already registered");
        }

        var user = new User
        {
            Username = username,
            Password = this._passwordService.GetHash(password)
        };

        await this._dbContext.Users.AddAsync(user);
        await this._dbContext.SaveChangesAsync();

        this.User = user;

        return await this.LoginAsync(username, password);
    }

    public async Task<Result> LoginAsync(string username, string password)
    {
        if (this.User is null)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user?.Password is null || !this._passwordService.ValidatePassword(password, user.Password))
            {
                return new("Invalid login");
            }

            this.User = user;
        }

        await this._httpContext.SignInAsync(Cookie, new(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{this.User.FirstName} {this.User.LastName}"),
            new Claim("ID", this.User.UserId.ToString()),
            new Claim("FirstName", this.User?.FirstName ?? string.Empty),
            new Claim("LastName", this.User?.LastName ?? string.Empty),
            new Claim("Username", this.User?.Username ?? string.Empty)
        }, Cookie)));

        return new();
    }

    public async Task LogoutAsync()
    {
        this.User = null;
        await this._httpContext.SignOutAsync(Cookie, new AuthenticationProperties { RedirectUri = $"/User/Login" });
    }
}