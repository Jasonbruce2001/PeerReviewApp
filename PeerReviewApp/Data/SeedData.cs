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
            var result = userManager.CreateAsync(testUser1, SECRET_PASSWORD);

            AppUser testUser2 = new AppUser { UserName = "Jason", AccountAge = date };
            result = userManager.CreateAsync(testUser2, SECRET_PASSWORD);

            AppUser testUser3 = new AppUser { UserName = "Travis", AccountAge = date };
            result = userManager.CreateAsync(testUser3, SECRET_PASSWORD);

            context.SaveChanges();
        }

        //Seeded Institutions 
        if (!context.Institution.Any())
        {
            context.Institution.AddRange(
                new Institution { Name = "Lane Community College" },
                new Institution { Name = "University Of Oregon" },
                new Institution { Name = "Linn Benton College" }
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
                        Instructor = user,
                        Term = "Winter 2025"
                    },
                    new Course
                    {
                        Name = "CS 233: Programming Concepts",
                        Instructor = user,
                        Term = "Spring 2025"
                    },
                      new Course
                      {
                          Name = "CS 275: Database Managment",
                          Instructor = user,
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
                        Uploader = user,
                        FilePath = "/uploads/syllabus.pdf"
                    },
                    new Document
                    {
                        Uploader = user,
                        FilePath = "/uploads/lecture_notes.docx"
                    },
                    new Document
                    {
                        Uploader = user,
                        FilePath = "/uploads/assignmentInstruction.pdf"
                    }
                );
                context.SaveChanges();
            }
        }

        // Seed Assignments
        if (!context.Assignments.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                context.Assignments.AddRange(
                    new Assignment
                    {
                        Course = context.Courses.FindAsync(1).Result,
                        DueDate = DateTime.Parse("11/21/2024"),
                        Title = "Lab 1",
                        Description = "Make a Project",
                        FilePath = "/uploads/assignmentInstruction.pdf"

                    },
                    new Assignment
                    {
                        Course = context.Courses.FindAsync(1).Result,
                        DueDate = DateTime.Parse("11/28/2024"),
                        Title = "Lab 2",
                        Description = "Make a Project Again",
                        FilePath = "/uploads/assignmentInstruction2.pdf"

                    },
                    new Assignment
                    {
                        Course = context.Courses.FindAsync(1).Result,
                        DueDate = DateTime.Parse("12/05/2024"),
                        Title = "Lab 1",
                        Description = "Make a Third Project",
                        FilePath = "/uploads/assignmentInstruction3.pdf"

                    }
                );
                context.SaveChanges();
            }
        }

        // Seed Grades
        if (!context.Grade.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                context.Grade.AddRange(
                    new Grade
                    {
                        Student = user,
                        Assignment = context.Assignments.Find(1),
                        Value = 94

                    },
                    new Grade
                    {
                        Student = user,
                        Assignment = context.Assignments.Find(2),
                        Value = 81

                    },
                    new Grade
                    {
                        Student = user,
                        Assignment = context.Assignments.Find(3),
                        Value = 54

                    }
                );
                context.SaveChanges();
            }
        }

        // Seed Group
        if (!context.Group.Any())
        {
            context.Group.AddRange(
                new Group
                {
                    Name = "Group 1",
                    Course = context.Courses.FirstOrDefaultAsync().Result
                },
                new Group
                {
                    Name = "Group 2",
                    Course = context.Courses.FirstOrDefaultAsync().Result
                },
                new Group
                {
                    Name = "Group 3",
                    Course = context.Courses.FirstOrDefaultAsync().Result
                }
            );
            context.SaveChanges();
        }

        // Seed GroupMembers
        if (!context.GroupMembers.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                context.GroupMembers.AddRange(
                    new GroupMembers
                    {
                        Group = context.Group.FindAsync(1).Result,
                        Member = user
                    },
                    new GroupMembers
                    {
                        Group = context.Group.FindAsync(2).Result,
                        Member = user
                    },
                    new GroupMembers
                    {
                        Group = context.Group.FindAsync(3).Result,
                        Member = user
                    }
                );
                context.SaveChanges();
            }
          
        if (!context.Courses.Any())
        {
            DateTime date = DateTime.Now;
            const string SECRET_PASSWORD = "Pass!123";
            AppUser instructor = new AppUser { UserName = "Instructor", AccountAge = date };
            var result = userManager.CreateAsync(instructor, SECRET_PASSWORD);

            Course course1 = new Course()
            {
                Name = "Course 1",
                Instructor = instructor,
                Term = "Spring"
            };
            
            context.Courses.Add(course1);
            context.SaveChanges();
        }
        
        if (!context.Assignments.Any())
        {
            DateTime date = DateTime.Now;
            const string SECRET_PASSWORD = "Pass!123";
            
            AppUser reviewer = new AppUser { UserName = "reviewer", AccountAge = date };
            var result = userManager.CreateAsync(reviewer, SECRET_PASSWORD);
            AppUser reviewee = new AppUser { UserName = "reviewee", AccountAge = date };
            result = userManager.CreateAsync(reviewee, SECRET_PASSWORD);

            Assignment assignment1 = new Assignment()
            {
                DueDate = date,
                ParentCourse = new Course() { Name = "Course 1", Instructor = reviewer, Term = "Spring"},
                Title = "Test Assignment",
                Description = "Test Description",
                FilePath = "/Assignments",
            };
            Assignment assignment2 = new Assignment()
            {
                DueDate = date,
                ParentCourse = new Course() { Name = "Course 2", Instructor = reviewer, Term = "Spring" },
                Title = "Test Assignment 2",
                Description = "Test Description 2",
                FilePath = "/Assignments",
            };
            Assignment assignment3 = new Assignment()
            {
                DueDate = date,
                ParentCourse = new Course() { Name = "Course 3", Instructor = reviewer, Term = "Spring" },
                Title = "Test Assignment 3",
                Description = "Test Description 3",
                FilePath = "/Assignments",
            };

            Review review1 = new Review()
            {
                ParentAssignment = assignment1,
                Reviewee = reviewee,
                Reviewer = reviewer,
                DueDate = date,
                FilePath = "/Reviews",
            };
            Review review2 = new Review()
            {
                ParentAssignment = assignment2,
                Reviewee = reviewee,
                Reviewer = reviewer,
                DueDate = date,
                FilePath = "/Reviews",
            };
            Review review3 = new Review()
            {
                ParentAssignment = assignment3,
                Reviewee = reviewee,
                Reviewer = reviewer,
                DueDate = date,
                FilePath = "/Reviews",
            };
            
            context.Assignments.Add(assignment1);
            context.Assignments.Add(assignment2);
            context.Assignments.Add(assignment3);
            
            context.Reviews.Add(review1);
            context.Reviews.Add(review2);
            context.Reviews.Add(review3);
            
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
        string email = "admin@admin.com";
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
            AppUser user = new AppUser { UserName = username, AccountAge = date, Email = email, InstructorCode = null}; 
            var result = await userManager.CreateAsync(user, password); 
            if (result.Succeeded) {
                await userManager.AddToRoleAsync(user, roleName); 
            }
        } 
    }
}
