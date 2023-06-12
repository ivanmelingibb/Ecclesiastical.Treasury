using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecclesiastical.Treasury.App.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class EcclesiasticalAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
        {
            context.Result = new ForbidResult();
        }
    }
}
