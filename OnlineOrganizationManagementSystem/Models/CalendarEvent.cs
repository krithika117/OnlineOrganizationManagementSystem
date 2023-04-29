﻿using System.ComponentModel.DataAnnotations;

namespace OnlineOrganizationManagementSystem.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string EventType { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Details { get; set; }
        public string? Scope { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
