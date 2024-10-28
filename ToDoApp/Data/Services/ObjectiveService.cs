using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public static class ObjectiveService
    {
        public static void AddObjective(User creator, string title, string description)
        {
            var objective = new Objective
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                CreatedDate = DateTime.Now,
                Creator = creator,
                IsDeleted = false,
                IsExecuted = false,
                ExecuteDate = null!
            };
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Objectives.Add(objective);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            Debug.WriteLine("Задача добавлена!");
        }
        public static void UpdateObjective(Objective objective, string title, string description, User assignee)
        {
            objective.Title = title;
            objective.Description = description;
            objective.Assignee = assignee;
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Objectives.Update(objective);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            Debug.WriteLine("Задача обновлена!");
        }
        public static void DeleteObjective(Objective objective)
        {
            using (var context = new SQLServerDBContext())
            {
                try
                {
                    context.Objectives.Remove(objective);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            Debug.WriteLine("Задача удалена!");
        }
        public static void Hide(Objective objective)
        {
            objective.IsDeleted = true;
            using (var context = new SQLServerDBContext())
            {
                context.Objectives.Update(objective);
                context.SaveChanges();
            }
            Debug.WriteLine("Задача скрыта!");
        }
        public static void Execute(Objective objective)
        {
            objective.IsExecuted = true;
            objective.ExecuteDate = DateTime.Now;
            using (var context = new SQLServerDBContext())
            {
                context.Objectives.Update(objective);
                context.SaveChanges();
            }
            Debug.WriteLine("Задача выполнена!");
        }
    }
}
