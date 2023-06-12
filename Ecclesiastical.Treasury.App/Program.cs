using Ecclesiastical.Treasury.App.Data;
using Ecclesiastical.Treasury.App.Models;
using Ecclesiastical.Treasury.App.Services;
using Ecclesiastical.Treasury.App.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<EcclesiasticalDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("EcclesiasticalDbConnectionString") ?? throw new InvalidOperationException("Missing connection string."));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Events = new EcclesiasticalAuthenticationEvents();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.Use(async (context, next) =>
{
    await next();

    if (context.Response is { StatusCode: >= 400 and <= 500, HasStarted: false })
    {
        context.Request.Path = $"/Error/{context.Response.StatusCode}";
        await next();
    }
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();