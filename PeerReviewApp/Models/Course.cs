using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeerReviewApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstutionId { get; set; }

        public string InstructorId { get; set; }

        // Make these nullable by adding the ? operator
        public virtual AppUser? Instructor { get; set; }
        public virtual ICollection<AppUser>? Students { get; set; }

        public string Term { get; set; }
    }
}
