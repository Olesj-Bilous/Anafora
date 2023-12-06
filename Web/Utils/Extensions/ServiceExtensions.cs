using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Utils.Extensions;
using AnaforaWeb.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AnaforaWeb.Utils.Extensions;

public static partial class ServiceExtensions
{
    public static async Task SeedDataContext(this IServiceProvider services, string adminEmail, string adminPassword)
    {
        await using var scope = services.CreateAsyncScope();

        IActionDescriptorCollectionProvider provider = scope.ServiceProvider.GetService<IActionDescriptorCollectionProvider>()!;
        var items = provider.ActionDescriptors.Items; // hook for content authorization policy (in development)

        UserManager<User> manager = scope.ServiceProvider.GetService<UserManager<User>>()!;

        DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Database.EnsureDeletedAsync(); // environment is currently assumed to be development
        await context.Database.EnsureCreatedAsync(); // an implementation with MigrateAsync should be provided for production
        await context.SeedAsync(manager, adminEmail, adminPassword);
    }

    public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
    {
        builder.Services.AddOptions<JwtAuthenticationOptions>()
            .Configure<ITicketStore>((options, store) =>
            {
                options.SessionStore = store;
                options.Events.OnTokenValidated = async context =>
                {
                    var id = context.Principal?.FindFirst(claim => claim.Type == "id");
                    if (id == null)
                    {
                        context.Fail($"{nameof(id)} Claim not found on Principal.");
                        return;
                    }
                    var ticket = await store.RetrieveAsync(id.Value);
                    if (ticket == null)
                    {
                        context.Fail("Session ticket not found in store.");
                        return;
                    }
                    context.Principal = ticket.Principal;
                    context.Properties = ticket.Properties;
                    context.Success();
                };
                configureOptions(options);
            });

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<
                IPostConfigureOptions<JwtBearerOptions>,
                JwtBearerPostConfigureOptions
            >()
        );
        return builder.AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>("JwtAuth", configureOptions);
    }
}
