using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeerReviewApp.Models
{
        public class AppUser : IdentityUser
        {
            [NotMapped]
            public IList<string> RoleNames { get; set; } = null!;

            public DateTime AccountAge { get; set; }
        }
}
