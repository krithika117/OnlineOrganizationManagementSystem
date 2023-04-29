﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineOrganizationManagementSystem.Models;

namespace OnlineOrganizationManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<CalendarEvent> CalendarEvent { get; set; }
    }
}