﻿using learnchess.Models;
using Microsoft.EntityFrameworkCore;

namespace learnchess.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<User>Users { get; set; } 

        public DbSet<Author>Authors { get; set; }

    }
}
