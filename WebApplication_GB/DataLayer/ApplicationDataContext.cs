﻿using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationDataContext: DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            
        }

        public DbSet<Contract> Contracts { get; set; }
    }
}