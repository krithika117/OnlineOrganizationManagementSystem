using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrganizationManagementSystem.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string EventType { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Details { get; set; }
       
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Teams Team { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
