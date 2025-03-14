namespace PeerReviewApp.Models;

public class Grade
{
    public int Id { get; set; }
    public AppUser Student { get; set; }
    public Assignment Assignment { get; set; }
    public int Value { get; set; }  
}