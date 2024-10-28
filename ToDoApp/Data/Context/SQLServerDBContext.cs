using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class SQLServerDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = ToDoDB;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Objective> Objectives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objective>()
                .HasOne(o => o.Creator)
                .WithMany(u => u.CreatedObjectives)
                .HasForeignKey(u => u.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Objective>()
                .HasOne(o => o.Assignee)
                .WithMany(u => u.AssignedObjectives)
                .HasForeignKey(u => u.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
