using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    // Общий вид БД для разлицных контекстов
    public abstract class AbstractContext : DbContext
    {
        // Модель таблицы Users
        public DbSet<User> Users { get; set; }

        // Модель таблицы Objectives
        public DbSet<Objective> Objectives { get; set; }

        // Настройка связей для таблиц
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
