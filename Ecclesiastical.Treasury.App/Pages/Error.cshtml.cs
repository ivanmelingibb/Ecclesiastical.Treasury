using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecclesiastical.Treasury.App.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }
    public string? ErrorType { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? Message { get; set; }
    
    public void OnGet(string type)
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        this.ErrorType = type;
        this.Message = type switch
        {
            "401" => "You are not logged in (Unauthenticated)",
            "403" => "You don't have permission to be here (Unauthorized)",
            "404" => "Page not found ()",
            _ => string.Empty
        };
    }
}