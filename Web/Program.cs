using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AnaforaData.Context;
using System.Security.Claims;
using AnaforaData.Model;
using AnaforaWeb.Authorization;
using AnaforaWeb.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using AnaforaWeb.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.")));

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddDistributedMemoryCache(); // adds default in-memory cache as IDistributedCache
builder.Services.AddSingleton<ITicketStore, TicketStore>(); // depends on IDistributedCache

builder.Services.AddCors(options => options.AddPolicy("DefaultCorsPolicy", builder => builder.SetIsOriginAllowed(origin => origin == "https://localhost:3000")));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies(identity => identity.ApplicationCookie.Configure<ITicketStore>((cookie, store) =>
    {
        cookie.SessionStore = store;
        cookie.LoginPath = "/Identity/Account/Login"; // for compatibility with Identity default UI
        cookie.Cookie.IsEssential = true;
    }));

builder.Services.AddSingleton<IAuthorizationHandler, ContentAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, ContentPolicyProvider>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy; // fall back to authentication
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

var seeding = app.Services.SeedDataContext("admin@anafora.net",
    // set password in project root dir: dotnet user-secrets set anafora-admin-password <password>
    builder.Configuration.GetValue<string>("anafora-admin-password") ?? throw new InvalidOperationException("Admin password not found."));

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("DefaultCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

await seeding;

app.Run();
