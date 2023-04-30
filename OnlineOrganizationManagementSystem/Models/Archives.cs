using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineOrganizationManagementSystem.Models
{
    public class Archives
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string UIUXDeveloperId { get; set; }

        public string FrontendDeveloperId { get; set; }        

        public string BackendDeveloperId { get; set; }        
        public string TesterId { get; set; }

        public string TeamLeadId { get; set; }
        public string ReportsToId { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime DateArchived { get; set; }
    }
}
