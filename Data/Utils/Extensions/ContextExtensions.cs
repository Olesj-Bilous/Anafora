using AnaforaData.Context;
using AnaforaData.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnaforaData.Utils.Extensions
{
    public static partial class ContextExtensions
    {
        public static async Task SeedAsync(this DataContext context, UserManager<User> manager, string adminEmail, string adminPassword)
        {
            await context.Database.MigrateAsync();
            await context.SeedIdentityAsync(manager, adminEmail, adminPassword);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public static async Task SeedIdentityAsync(this DataContext context, UserManager<User> manager, string adminEmail, string adminPassword)
        {
            User user = await context.Users.FirstOrDefaultAsync(i => i.UserName == adminEmail);
            if (user == null)
            {
                user = (await context.Users.AddAsync(new()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                })).Entity;
                await manager.CreateAsync(user, adminPassword);
            }
        }
    }
}