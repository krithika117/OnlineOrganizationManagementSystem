﻿using System.ComponentModel.DataAnnotations;

namespace OnlineOrganizationManagementSystem.Models
{
    public class PublicHoliday
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
