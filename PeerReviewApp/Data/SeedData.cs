using PeerReviewApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace PeerReviewApp.Data;
public class SeedData
{
    public static async Task Seed(ApplicationDbContext context, IServiceProvider provider)
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
            var result = await userManager.CreateAsync(testUser1, SECRET_PASSWORD);

            AppUser testUser2 = new AppUser { UserName = "Jason", AccountAge = date };
            result = await userManager.CreateAsync(testUser2, SECRET_PASSWORD);

            AppUser testUser3 = new AppUser { UserName = "Travis", AccountAge = date };
            result = await userManager.CreateAsync(testUser3, SECRET_PASSWORD);
        }

        //Seeded Institutions 
        if (!context.Institution.Any())
        {
            context.Institution.AddRange(
                new Institution { Name = "Lane Community College" },
                new Institution { Name = "University Of Oregon" },
                new Institution { Name = "Linn Benton College"}
            );
            context.SaveChanges();
        }

        // Seed Courses
        if (!context.Courses.Any())
        {
            
            var user = await userManager.Users.FirstOrDefaultAsync();
            var institution = context.Institution.FirstOrDefault();
            if (user != null && institution != null)
            {
                context.Courses.AddRange(
                    new Course
                    {
                        Name = "CS 246: System Design",
                        InstructorId = user.Id,
                        Term = "Winter 2025"
                    },
                    new Course
                    {
                        Name = "CS 233: Programming Concepts",
                        InstructorId = user.Id,
                        Term = "Spring 2025"
                    },
                      new Course
                      {
                          Name = "CS 275: Database Managment",
                          InstructorId = user.Id,
                          Term = "Spring 2025"
                      }
                );
                context.SaveChanges();
            }
        }

        // Seed Documents
        if (!context.Document.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                context.Document.AddRange(
                    new Document
                    {
                        UploaderId = user.Id,
                        FilePath = "/uploads/syllabus.pdf"
                    },
                    new Document
                    {
                        UploaderId = user.Id,
                        FilePath = "/uploads/lecture_notes.docx"
                    },
                    new Document
                    {
                        UploaderId = user.Id,
                        FilePath = "/uploads/assignmentInstruction.pdf"
                    }
                );
                context.SaveChanges();
            }
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
        string email = "admin@admin.com";
        string password = "Pass1234!";
        string roleName = "Admin";
        DateTime date = DateTime.Now;

      
        if (await roleManager.FindByNameAsync(roleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
       
        if (await userManager.FindByNameAsync(username) == null)
        {
            AppUser user = new AppUser { UserName = username, AccountAge = date, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}