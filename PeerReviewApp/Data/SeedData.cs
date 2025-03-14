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
            await userManager.CreateAsync(testUser1, SECRET_PASSWORD);

            AppUser testUser2 = new AppUser { UserName = "Jason", AccountAge = date };
            await userManager.CreateAsync(testUser2, SECRET_PASSWORD);

            AppUser testUser3 = new AppUser { UserName = "Travis", AccountAge = date };
            await userManager.CreateAsync(testUser3, SECRET_PASSWORD);

            AppUser instructor = new AppUser { UserName = "Instructor", AccountAge = date };
            await userManager.CreateAsync(instructor, SECRET_PASSWORD);

            AppUser reviewer = new AppUser { UserName = "reviewer", AccountAge = date };
            await userManager.CreateAsync(reviewer, SECRET_PASSWORD);

            AppUser reviewee = new AppUser { UserName = "reviewee", AccountAge = date };
            await userManager.CreateAsync(reviewee, SECRET_PASSWORD);

            await context.SaveChangesAsync();
        }

        //Seeded Institutions 
        if (!context.Institution.Any())
        {
            context.Institution.AddRange(
                new Institution { Name = "Lane Community College" },
                new Institution { Name = "University Of Oregon" },
                new Institution { Name = "Linn Benton College" }
            );
            await context.SaveChangesAsync();
        }

        // Seed Courses
        if (!context.Courses.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();
            var instructor = await userManager.FindByNameAsync("Instructor");
            var actualInstructor = instructor ?? user;
            var institution = context.Institution.FirstOrDefault();

            if (actualInstructor != null)
            {
                context.Courses.AddRange(
                    new Course
                    {
                        Name = "CS 246: System Design",
                        Instructor = actualInstructor,
                        InstructorId = actualInstructor.Id,
                        Term = "Winter 2025",
                        Students = new List<AppUser>()
                    },
                    new Course
                    {
                        Name = "CS 233: Programming Concepts",
                        Instructor = actualInstructor,
                        InstructorId = actualInstructor.Id,
                        Term = "Spring 2025",
                        Students = new List<AppUser>()
                    },
                    new Course
                    {
                        Name = "CS 275: Database Management",
                        Instructor = actualInstructor,
                        InstructorId = actualInstructor.Id,
                        Term = "Spring 2025",
                        Students = new List<AppUser>()
                    },
                    new Course
                    {
                        Name = "Course 1",
                        Instructor = actualInstructor,
                        InstructorId = actualInstructor.Id,
                        Term = "Spring",
                        Students = new List<AppUser>()
                    }
                );
                await context.SaveChangesAsync();
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
                        UploaderId = user.Id,
                        FilePath = "/uploads/syllabus.pdf"
                    },
                    new Document
                    {
                        Uploader = user,
                        UploaderId = user.Id,
                        FilePath = "/uploads/lecture_notes.docx"
                    },
                    new Document
                    {
                        Uploader = user,
                        UploaderId = user.Id,
                        FilePath = "/uploads/assignmentInstruction.pdf"
                    }
                );
                await context.SaveChangesAsync();
            }
        }

        // Seed Assignments
        if (!context.Assignments.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();
            var reviewer = await userManager.FindByNameAsync("reviewer");
            var actualReviewer = reviewer ?? user;
            var courses = await context.Courses.ToListAsync();

            if (user != null && courses.Any())
            {
                // First set from Aiden's branch
                context.Assignments.AddRange(
                    new Assignment
                    {
                        CourseId = courses[0].Id.ToString(),
                        DueDate = DateTime.Parse("11/21/2024"),
                        Title = "Lab 1",
                        Description = "Make a Project",
                        FilePath = "/uploads/assignmentInstruction.pdf"
                    },
                    new Assignment
                    {
                        CourseId = courses[0].Id.ToString(),
                        DueDate = DateTime.Parse("11/28/2024"),
                        Title = "Lab 2",
                        Description = "Make a Project Again",
                        FilePath = "/uploads/assignmentInstruction2.pdf"
                    },
                    new Assignment
                    {
                        CourseId = courses[0].Id.ToString(),
                        DueDate = DateTime.Parse("12/05/2024"),
                        Title = "Lab 3",
                        Description = "Make a Third Project",
                        FilePath = "/uploads/assignmentInstruction3.pdf"
                    }
                );

                // Create separate objects for the second set of assignments
                // Use a different course if available
                var coursesCount = courses.Count;
                var courseForSet2 = coursesCount > 3 ? courses[3] : courses[0]; // Use "Course 1" if available

                DateTime date = DateTime.Now;

                var assignment1 = new Assignment()
                {
                    DueDate = date,
                    CourseId = courseForSet2.Id.ToString(),
                    Title = "Test Assignment",
                    Description = "Test Description",
                    FilePath = "/Assignments",
                };

                var assignment2 = new Assignment()
                {
                    DueDate = date,
                    CourseId = courseForSet2.Id.ToString(),
                    Title = "Test Assignment 2",
                    Description = "Test Description 2",
                    FilePath = "/Assignments",
                };

                var assignment3 = new Assignment()
                {
                    DueDate = date,
                    CourseId = courseForSet2.Id.ToString(),
                    Title = "Test Assignment 3",
                    Description = "Test Description 3",
                    FilePath = "/Assignments",
                };

                context.Assignments.Add(assignment1);
                context.Assignments.Add(assignment2);
                context.Assignments.Add(assignment3);

                await context.SaveChangesAsync();

                // Now create reviews for the second set of assignments
                var reviewee = await userManager.FindByNameAsync("reviewee");
                var actualReviewee = reviewee ?? user;

                if (actualReviewer != null && actualReviewee != null)
                {
                    // Note: Review model has private DueDate and FilePath
                    // If this causes issues, you'll need to modify the Review model

                    var review1 = new Review()
                    {
                        AssignmentId = assignment1.Id,
                        Reviewer = actualReviewer,
                        Reviewee = actualReviewee
                        // Can't set DueDate and FilePath as they're private
                    };

                    var review2 = new Review()
                    {
                        AssignmentId = assignment2.Id,
                        Reviewer = actualReviewer,
                        Reviewee = actualReviewee
                        // Can't set DueDate and FilePath as they're private
                    };

                    var review3 = new Review()
                    {
                        AssignmentId = assignment3.Id,
                        Reviewer = actualReviewer,
                        Reviewee = actualReviewee
                        // Can't set DueDate and FilePath as they're private
                    };

                    context.Reviews.Add(review1);
                    context.Reviews.Add(review2);
                    context.Reviews.Add(review3);

                    await context.SaveChangesAsync();
                }
            }
        }

        // Seed Grades
        if (!context.Grade.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();
            var assignments = await context.Assignments.ToListAsync();

            if (user != null && assignments.Count >= 3)
            {
                context.Grade.AddRange(
                    new Grade
                    {
                        Student = user,
                        AssignmentId = assignments[0].Id,
                        Value = 94
                    },
                    new Grade
                    {
                        Student = user,
                        AssignmentId = assignments[1].Id,
                        Value = 81
                    },
                    new Grade
                    {
                        Student = user,
                        AssignmentId = assignments[2].Id,
                        Value = 54
                    }
                );
                await context.SaveChangesAsync();
            }
        }

        // Seed Group
        if (!context.Group.Any())
        {
            var courses = await context.Courses.ToListAsync();

            if (courses.Any())
            {
                context.Group.AddRange(
                    new Group
                    {
                        Name = "Group 1",
                        CourseId = courses[0].Id
                    },
                    new Group
                    {
                        Name = "Group 2",
                        CourseId = courses[0].Id
                    },
                    new Group
                    {
                        Name = "Group 3",
                        CourseId = courses[0].Id
                    }
                );
                await context.SaveChangesAsync();
            }
        }

        // Seed GroupMembers
        if (!context.GroupMembers.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync();
            var groups = await context.Group.ToListAsync();

            if (user != null && groups.Any())
            {
                context.GroupMembers.AddRange(
                    new GroupMembers
                    {
                        Group = groups[0],
                        Member = user
                    },
                    new GroupMembers
                    {
                        Group = groups.Count > 1 ? groups[1] : groups[0],
                        Member = user
                    },
                    new GroupMembers
                    {
                        Group = groups.Count > 2 ? groups[2] : groups[0],
                        Member = user
                    }
                );
                await context.SaveChangesAsync();
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

        // if role doesn't exist, create it
        if (await roleManager.FindByNameAsync(roleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        // if username doesn't exist, create it and add it to role
        if (await userManager.FindByNameAsync(username) == null)
        {
            AppUser user = new AppUser { UserName = username, AccountAge = date, Email = email, InstructorCode = null };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}