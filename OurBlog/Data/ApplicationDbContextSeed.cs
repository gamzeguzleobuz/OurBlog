using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OurBlog.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            var adminEmail = "admin@example.com";
            var adminPass = "Ankara1.";
            var adminRoleName = "Administrator";

            if (await userManager.Users.AnyAsync(x => x.UserName == adminEmail) ||
                await roleManager.RoleExistsAsync(adminRoleName))
                return;

            var adminUser = new IdentityUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, adminPass);
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            await userManager.AddToRoleAsync(adminUser, adminRoleName);

            List<Post> posts = new List<Post>
            {
                new Post
                {
                    Title = "Captivated by the Dance of the Northern Lights",
                    Content = "A mesmerizing display of colors across the Arctic skies. Nature's masterpiece at its finest.",
                    Image = "1.jpg",
                    AuthorId = adminUser.Id
                },
                new Post
                {
                    Title = "Moonlit Serenity: Embracing the Night's Radiance",
                    Content = "Basking in the tranquil glow of the moon's light, a moment of reflection and peace.",
                    Image = "2.jpg",
                    AuthorId = adminUser.Id
                },
                new Post
                {
                   Title = "Elegance in Every Petal: A Celebration of Peonies",
                    Content = "Capturing the timeless beauty of peonies in a vibrant bouquet. A tribute to nature's artistry.",
                    Image = "3.jpg",
                    AuthorId = adminUser.Id
                }
            };

            db.AddRange(posts);
            db.SaveChanges();
        }
    }
}
