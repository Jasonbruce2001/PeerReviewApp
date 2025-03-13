using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeerReviewApp.Models
{
    public class GroupMembers
    {
        public int GroupMembersId { get; set; }

        public Group Group { get; set; } //This won't be an error when merged with other branches

        public AppUser Member { get; set; }
    }
}
