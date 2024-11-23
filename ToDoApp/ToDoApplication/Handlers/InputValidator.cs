using Data.Handlers;
using Data.Models;
using LogHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApplication.Notifiers;

namespace ToDoApplication.Handlers
{
    // Проверка данных на корректность
    internal static class InputValidator
    {
        // Проверка исходя из типа данных
        internal static bool IsValid<T>(T entity)
        {
            var result = false;
            switch (entity)
            {
                case User:
                    result = IsUserValid((entity as User)!);
                    break;
                case Objective:
                    result = IsObjectiveValid((entity as Objective)!);
                    break;
            }
            return result;
        }
        // Проверка User
        private static bool IsUserValid(User user)
        {
            if (InputHandler.Handle(user))
            {
                Logger.AddLog($"Ввод пользователя {user.GetType().Name} ({user.Fullname}) корректен!");
                return true;
            }
            else
            {
                MessageNotifier.Error(Logger.AddLog($"Ввод пользователя {user.GetType().Name} некорректен!"));
                return false;
            }
        }
        // Проверка Objective
        private static bool IsObjectiveValid(Objective objective)
        {
            if (InputHandler.Handle(objective))
            {
                Logger.AddLog($"Задача {objective.Title.GetType().Name} ({objective.Title}) корректна!");
                return true;
            }
            else
            {
                MessageNotifier.Error(Logger.AddLog($"Данные задачи не корректны!"));
                return false;
            }
        }
    }
}
