namespace PeerReviewApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstutionId { get; set; }
        public int InstructorId { get; set; }
        public string Term { get; set; }
        IList<AppUser> Students { get; set; }
    }
}
