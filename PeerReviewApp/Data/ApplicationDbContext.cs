using Microsoft.AspNetCore.Identity;
using PeerReviewApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PeerReviewApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    // constructor just calls the base class constructor
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Institution> Institution { get; set; } = default!;

    // one DbSet for each domain model class
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; } = default!;
    public DbSet<Document> Document { get; set; } = default!;
    public DbSet<Grade> Grade { get; set; } = default!;
    public DbSet<PeerReviewApp.Models.Group> Group { get; set; } = default!;
    public DbSet<PeerReviewApp.Models.GroupMembers> GroupMembers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Course relationships
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many relationship between Course and Student
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithMany()
            .UsingEntity(j => j.ToTable("CourseStudent"));
    }
}