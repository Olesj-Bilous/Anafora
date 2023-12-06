using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaWeb.Authentication;
using AnaforaWeb.Authorization;
using AnaforaWeb.Utils;
using AnaforaWeb.Utils.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<DataContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DataContextConnection")
        ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.")
    )
);

var allowedOrigin = builder.Configuration["AllowedHosts"];
var clientPort = builder.Configuration["ClientPort"];
if (clientPort != null) allowedOrigin += $":{clientPort}";

builder.Services.AddCors(options => options.AddPolicy(
    "DefaultCorsPolicy",
    policy => policy.SetIsOriginAllowed(
        origin => origin == $"https://{allowedOrigin}"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    )
);

builder.Services.AddSession();

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.ClaimsIdentity.UserIdClaimType = ClaimsPrincipalFactory.UserIdClaimType;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
    .AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache(); // adds in-memory cache as IDistributedCache stub
builder.Services.AddSingleton<ITicketStore, TicketStore>(); // uses IDistributedCache to store session tickets

builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidIssuer = "https://localhost:7166", // https should come first
        ValidAudience = allowedOrigin,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]
            )
        ),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, ContentAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, ContentPolicyProvider>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy; // fall back to authentication
});

builder.Services.AddControllers();


var app = builder.Build();

var seeding = app.Services.SeedDataContext(
    "admin@anafora.net",
    // set password in project root dir: dotnet user-secrets set anafora-admin-password <password>
    builder.Configuration.GetValue<string>("anafora-admin-password")
    ?? throw new InvalidOperationException("Admin password not found.")
);

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("DefaultCorsPolicy");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await seeding;

app.Run();
