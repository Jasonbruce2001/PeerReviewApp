using Microsoft.AspNetCore.Identity;
using PeerReviewApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PeerReviewApp.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    // constructor just calls the base class constructor
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Institution> Institution { get; set; } = default!;
    
    // one DbSet for each domain model class
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Course> Courses { get; set; }

public DbSet<PeerReviewApp.Models.Assignmnet> Assignmnet { get; set; } = default!;
}