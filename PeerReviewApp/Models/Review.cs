namespace PeerReviewApp.Models;

public class Review
{
    public int Id { get; set; }
    public Assignment ParentAssignment { get; set; }
    public AppUser Reviewer { get; set; }
    public AppUser Reviewee { get; set; }
    public DateTime DueDate { get; set; }
    public String FilePath { get; set; }
}