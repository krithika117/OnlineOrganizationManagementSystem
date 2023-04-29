using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrganizationManagementSystem.Models
{
    public class Notes
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Priority { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public IdentityUser User;
    }
}