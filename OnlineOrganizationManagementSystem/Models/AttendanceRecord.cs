using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrganizationManagementSystem.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        [Required]
        public string PresenceStatus { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DateRecord { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public IdentityUser User;
    }
}
