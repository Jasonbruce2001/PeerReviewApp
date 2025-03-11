using PeerReviewApp.Models;
using Microsoft.AspNetCore.Identity;

namespace PeerReviewApp.Data;

public class SeedData
{
    public static void Seed(ApplicationDbContext context, IServiceProvider provider)
    {
        var userManager = provider
            .GetRequiredService<UserManager<AppUser>>();

        //only add users if there are none
        if (!userManager.Users.Any())
        {
            // Create User objects
            DateTime date = DateTime.Now;
            const string SECRET_PASSWORD = "Pass!123";
            AppUser testUser1 = new AppUser { UserName = "Aiden", AccountAge = date };
            var result = userManager.CreateAsync(testUser1, SECRET_PASSWORD);
            
            AppUser testUser2 = new AppUser { UserName = "Jason", AccountAge = date };
            result = userManager.CreateAsync(testUser2, SECRET_PASSWORD);

            AppUser testUser3 = new AppUser { UserName = "Travis", AccountAge = date };
            result = userManager.CreateAsync(testUser3, SECRET_PASSWORD);

            context.SaveChanges();
        }
    }

    //create admin user
    public static async Task CreateAdminUser(IServiceProvider serviceProvider) 
    {
        UserManager<AppUser> userManager =
            serviceProvider.GetRequiredService<UserManager<AppUser>>(); 
        RoleManager<IdentityRole> roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        string username = "Administrator"; 
        string password = "Pass1234!"; 
        string roleName = "Admin";
        DateTime date = DateTime.Now;
        
        // if role doesn't exist, create it
        if (await roleManager.FindByNameAsync(roleName) == null) 
        { 
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        // if username doesn't exist, create it and add it to role
        if (await userManager.FindByNameAsync(username) == null) 
        { 
            AppUser user = new AppUser { UserName = username, AccountAge = date}; 
            var result = await userManager.CreateAsync(user, password); 
            if (result.Succeeded) {
                await userManager.AddToRoleAsync(user, roleName); 
            }
        } 
    }
}
