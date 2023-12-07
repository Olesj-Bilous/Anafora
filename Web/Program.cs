using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaWeb.Authentication;
using AnaforaWeb.Routes;
using AnaforaWeb.Utils;
using AnaforaWeb.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.")
));

builder.Services.AddIdentityCore<User>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = ClaimsPrincipalFactory.UserIdClaimType;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
    .AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache(); // adds in-memory cache as IDistributedCache stub
builder.Services.AddSingleton<ITicketStore, TicketStore>(); // uses IDistributedCache to store session tickets



builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("pageroute", typeof(PageRouteConstraint)));

builder.Services.AddControllers();
builder.Services.AddReverseProxy().LoadFromConfig(
    builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

var seeding = app.Services.SeedDataContext(
    "admin@anafora.net",
    // in project root dir: dotnet user-secrets set anafora-admin-password <password>
    builder.Configuration.GetValue<string>("anafora-admin-password") ?? throw new InvalidOperationException("Admin password not found.")
);

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapReverseProxy();

await seeding;

app.Run();
