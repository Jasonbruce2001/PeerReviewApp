namespace PeerReviewApp.Models;

public class Review
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public AppUser Reviewer { get; set; }
    public AppUser Reviewee { get; set; }
    DateTime DueDate { get; set; }
    String FilePath { get; set; }
}