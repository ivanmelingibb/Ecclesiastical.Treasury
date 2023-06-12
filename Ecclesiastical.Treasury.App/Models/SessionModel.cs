using Ecclesiastical.Treasury.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecclesiastical.Treasury.App.Models;

public class SessionModel : PageModel
{
    protected ISessionService SessionService { get; }

    public SessionModel(ISessionService sessionService) : base()
    {
        SessionService = sessionService;
    }
}