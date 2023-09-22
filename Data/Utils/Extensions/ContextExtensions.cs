using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using AnaforaData.Utils.Dynamics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AnaforaData.Utils.Extensions
{
    public static partial class ContextExtensions
    {
        public static async Task SeedAsync(this DataContext context, UserManager<User> manager, string adminEmail, string adminPassword)
        {
            await context.SeedIdentityAsync(manager, adminEmail, adminPassword);
            await context.SeedProductStringValuesAsync();
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

        public static async Task SeedProductStringValuesAsync(this DataContext context)
        {
            ProductStringProperty color = new() { Name = "color" };
            ProductStringProperty material = new() { Name = "material" };
            ProductStringProperty size = new() { Name="size"};
            ProductStringProperty scent = new() { Name = "scent" };
            ProductStringValue[] colors = {
                new() { Property = color, Value = "green" },
                new() { Property = color, Value = "blue" },
                new() { Property = color, Value = "red" }
            };
            ProductStringValue[] materials = {
                new() { Property = material, Value = "leather" },
                new() { Property = material, Value = "linnen" },
                new() { Property = material, Value = "jeans" }
            };
            ProductStringValue[] sizes = {
                new() { Property = size, Value = "small" },
                new() { Property = size, Value = "medium" },
                new() { Property = size, Value = "large" }
            };
            ProductStringValue[] scents = {
                new() { Property = scent,Value="wood"},
                new() { Property = scent,Value="vanille"},
                new() { Property = scent,Value="summer"}
            };
            ProductType fashion = new() { Name = "fashion" };
            ProductType perfumes = new() { Name = "perfumes" };
            ProductStringPropertyType[] propertyTypes = {
                new() { Property=scent, Type=perfumes},
                new() { Property=size, Type=perfumes},
                new() {Property=size,Type=fashion},
                new() { Property=color,Type=fashion},
                new() {Property=material,Type=fashion}
            };
            await context.AddRangeAsync(color, material, size, scent);
            await context.AddRangeAsync(colors);
            await context.AddRangeAsync(materials);
            await context.AddRangeAsync(sizes);
            await context.AddRangeAsync(scents);
            await context.AddRangeAsync(fashion, perfumes);
            await context.AddRangeAsync(propertyTypes);
        }
    }
}