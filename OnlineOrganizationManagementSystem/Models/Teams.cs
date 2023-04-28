using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Teams
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    
    public string UIUXDeveloper { get; set; }
    public string FrontendDeveloper { get; set; }
    public string BackendDeveloper { get; set; }
    public string Tester { get; set; }
    public string TeamLead { get; set; }
    public string ReportsTo { get; set; }



}