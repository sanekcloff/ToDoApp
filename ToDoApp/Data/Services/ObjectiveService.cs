using Data.Context;
using Data.Models;
using LogHandler;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    // Класс для работы с Objective
    public static class ObjectiveService
    {
        // Добавление
        public async static void Add(Objective objective)
        {
            try
            {
                DbWorker.AbstractContext.Objectives.Add(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) добавлена!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }

        // Обновление
        public async static void Update(Objective objective, string title, string description, User assigner)
        {
            objective.Title = title;
            objective.Description = description;
            objective.Assignee = assigner;
            try
            {
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) обновлена!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }

        // Удаление
        public async static void Delete(Objective objective)
        {
            try
            {
                DbWorker.AbstractContext.Objectives.Remove(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) удалена!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }

        // Скрыть
        public async static void Hide(Objective objective)
        {
            if (objective.IsDeleted == false)
            {
                objective.IsDeleted = true;
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) скрыта!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Ошибка! Задача ({objective.Title}) уже скрыта!");
            }
        }

        // Сделать видимым
        public async static void Show(Objective objective)
        {
            if (objective.IsDeleted == true)
            {
                objective.IsDeleted = false;
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) теперь видима!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Ошибка! Задача ({objective.Title}) уже видима!");
            }
            
        }

        // Выполнить
        public async static void Execute(Objective objective)
        {
            if (objective.IsExecuted == false)
            {
                objective.IsExecuted = true;
                objective.ExecuteDate = DateTime.Now;
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) выполнена!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Ошбика! Задача ({objective.Title}) уже выполнена!");
            }
            
        }

        // Отменить
        public async static void Undo(Objective objective)
        {
            if (objective.IsExecuted == true)
            {
                objective.IsExecuted = false;
                objective.ExecuteDate = null;
                DbWorker.AbstractContext.Objectives.Update(objective);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Задача ({objective.Title}) отменена!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Ошбика! Задача ({objective.Title}) уже отменена!");
            }

        }
    }
}
