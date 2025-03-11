﻿namespace PeerReviewApp.Models

{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstutionId { get; set; }
        public virtual AppUser Instructor { get; set; }
        public string Term { get; set; }
        public virtual ICollection<AppUser> Students { get; set; }
    }
}
