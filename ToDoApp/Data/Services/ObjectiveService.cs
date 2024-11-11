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
        public static void AddObjective(User creator, User assigner, string title, string description)
        {
            var objective = new Objective
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                CreatedDate = DateTime.Now,
                Creator = creator,
                Assignee = assigner,
                IsDeleted = false,
                IsExecuted = false,
                ExecuteDate = null!
            };
            try
            {
                DbWorker.AbstractContext.Objectives.Add(objective);
                DbWorker.AbstractContext.SaveChanges();
                Debug.WriteLine($"{DbWorker.AbstractContext.GetType().Name}: Задача добавлена!");
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }
        public static void UpdateObjective(Objective objective, string title, string description, User assigner)
        {
            objective.Title = title;
            objective.Description = description;
            objective.Assignee = assigner;
            try
            {
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                Debug.WriteLine($"{DbWorker.AbstractContext.GetType().Name}: Задача обновлена!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void DeleteObjective(Objective objective)
        {
            try
            {
                DbWorker.AbstractContext.Objectives.Remove(objective);
                DbWorker.AbstractContext.SaveChanges();
                Debug.WriteLine($"{DbWorker.AbstractContext.GetType().Name}: Задача удалена!");
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
        }
        public static void Hide(Objective objective)
        {
            objective.IsDeleted = true;
            DbWorker.AbstractContext.Objectives.Update(objective);
            DbWorker.AbstractContext.SaveChanges();
            Debug.WriteLine($"{DbWorker.AbstractContext.GetType().Name}: Задача скрыта!");
        }
        public static void Execute(Objective objective)
        {
            objective.IsExecuted = true;
            objective.ExecuteDate = DateTime.Now;
            DbWorker.AbstractContext.Objectives.Update(objective);
            DbWorker.AbstractContext.SaveChanges();
            Debug.WriteLine($"{DbWorker.AbstractContext.GetType().Name}: Задача выполнена!");
        }
    }
}
