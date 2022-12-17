using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Seeding;
using AnaforaData.Utils.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnaforaWeb.Utils.Extensions;

public static class ServiceExtensions
{
    public static async Task SeedDataContext(this IServiceProvider services, string adminEmail, string adminPassword)
    {
        await using var scope = services.CreateAsyncScope();

        DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();

        IActionDescriptorCollectionProvider provider = scope.ServiceProvider.GetService<IActionDescriptorCollectionProvider>();
        var items = provider.ActionDescriptors.Items; // hook for content authorization policy (in development)

        UserManager<User> manager = scope.ServiceProvider.GetService<UserManager<User>>();
        await context.SeedAsync(manager, adminEmail, adminPassword);
    }
}
