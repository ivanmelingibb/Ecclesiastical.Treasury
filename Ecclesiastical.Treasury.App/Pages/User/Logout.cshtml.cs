using Ecclesiastical.Treasury.App.Models;
using Ecclesiastical.Treasury.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecclesiastical.Treasury.App.Pages.User;

public class Logout : SessionModel
{
    public Logout(ISessionService session) : base(session)
    {
    }

    public async Task OnGetAsync()
    {
        await this.SessionService.LogoutAsync();
    }
}
