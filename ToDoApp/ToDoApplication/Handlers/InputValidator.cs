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
    internal static class InputValidator
    {
        internal static bool IsValid<T>([NotNull] T entity)
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
        private static bool IsUserValid(User user)
        {
            if (InputHandler.Handle(user))
            {
                Logger.AddLog($"Данные пользователя {user.Fullname} корректны!");
                return true;
            }
            else
            {
                MessageNotifier.Error(Logger.AddLog($"Данные пользователя не корректны!"));
                return false;
            }
        }
        private static bool IsObjectiveValid(Objective objective)
        {
            if (InputHandler.Handle(objective))
            {
                Logger.AddLog($"Задача {objective.Title} корректна!");
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
