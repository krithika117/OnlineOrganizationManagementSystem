using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineOrganizationManagementSystem.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public float Amount { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Teams Team { get; set; }
    }
}
