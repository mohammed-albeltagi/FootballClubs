using Microsoft.AspNetCore.Identity;

namespace FootballClubs.Models
{
    public static class DataSeed
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                User user = new User
                {
                    UserName = "admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "admin").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByNameAsync("user").Result == null)
            {
                User user = new User
                {
                    UserName = "user"
                };

                IdentityResult result = userManager.CreateAsync(user, "user").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}
