using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class Teams
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [ForeignKey("UIUXDeveloper")]
    public string UIUXDeveloperId { get; set; }
    public IdentityUser UIUXDeveloper;


    [ForeignKey("FrontendDeveloper")]
    public string FrontendDeveloperId { get; set; }
    public IdentityUser FrontendDeveloper;


    [ForeignKey("BackendDeveloper")]
    public string BackendDeveloperId { get; set; }
    public IdentityUser BackendDeveloper;

    [ForeignKey("Tester")]
    public string TesterId { get; set; }
    public IdentityUser Tester;

    [ForeignKey("TeamLead")]
    public string TeamLeadId { get; set; }

    public IdentityUser TeamLead;

    [ForeignKey("ReportsTo")]
    public string ReportsToId { get; set; }

    public IdentityUser ReportsTo;

}