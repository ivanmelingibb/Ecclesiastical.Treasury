using System.Security.Claims;
using Ecclesiastical.Treasury.App.Data;
using Ecclesiastical.Treasury.App.Models;
using Ecclesiastical.Treasury.App.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecclesiastical.Treasury.App.Pages.User;

public class Login : SessionModel
{
    public Login(ISessionService sessionService) : base(sessionService)
    {
        
    }

    [BindProperty]
    public string? Username { get; set; }
    [BindProperty]
    public string? Password { get; set; }
    [BindProperty]
    public bool RememberMe { get; set; }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (this.Username is null)
        {
            return this.Page();
        }

        if (this.Password is null)
        {
            return this.Page();
        }
        
        var result = await this.SessionService.LoginAsync(this.Username, this.Password);
        if (result.Error)
        {
            return this.Page();
        }

        return this.Redirect($"/");
    }
}