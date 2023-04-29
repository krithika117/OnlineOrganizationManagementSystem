using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineOrganizationManagementSystem.Models
{

    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Assignee")]
        public string AssigneeId { get; set; }
        public IdentityUser Assignee { get; set; }       
        
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public IdentityUser Manager { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }


        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Teams Team { get; set; }
    }
}


