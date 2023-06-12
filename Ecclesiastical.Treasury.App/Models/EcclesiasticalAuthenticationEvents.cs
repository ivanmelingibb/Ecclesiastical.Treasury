using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecclesiastical.Treasury.App.Models;

public class EcclesiasticalAuthenticationEvents: CookieAuthenticationEvents
{
    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/User/Login";
        return base.RedirectToLogin(context);
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.RedirectUri = $"/User/Login";
        return base.RedirectToAccessDenied(context);
    }
}