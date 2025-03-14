using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeerReviewApp.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string UploaderId { get; set; }
        public virtual AppUser Uploader { get; set; }
        public string FilePath { get; set; }
    }
}
