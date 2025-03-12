<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeerReviewApp.Models
=======
﻿namespace PeerReviewApp.Models

>>>>>>> cabfe0a005c1989b096de0e723f6301acef3618d
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstutionId { get; set; }
<<<<<<< HEAD

        public string InstructorId { get; set; }

        // Make these nullable by adding the ? operator
        public virtual AppUser? Instructor { get; set; }
        public virtual ICollection<AppUser>? Students { get; set; }

        public string Term { get; set; }
=======
        public virtual AppUser Instructor { get; set; }
        public string Term { get; set; }
        public virtual ICollection<AppUser> Students { get; set; }
>>>>>>> cabfe0a005c1989b096de0e723f6301acef3618d
    }
}
