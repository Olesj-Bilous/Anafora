using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Seeding;
using AnaforaData.Utils.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnaforaWeb.Utils.Extensions;

public static class ServiceExtensions
{
    public static async Task SeedDataContext(this IServiceProvider services, string adminEmail, string adminPassword)
    {
        await using var scope = services.CreateAsyncScope();

        DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var migration = context.Database.MigrateAsync();

        UserManager<User> manager = scope.ServiceProvider.GetService<UserManager<User>>();
        IActionDescriptorCollectionProvider provider = scope.ServiceProvider.GetService<IActionDescriptorCollectionProvider>();

        await migration;
        await context.SeedAsync(manager, provider, adminEmail, adminPassword).ConfigureAwait(false);
    }
}
