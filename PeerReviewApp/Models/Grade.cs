namespace PeerReviewApp.Models;

public class Grade
{
    public int Id { get; set; }
    public AppUser Student { get; set; }
    public int AssignmentId { get; set; }
    public int Value { get; set; }  
}