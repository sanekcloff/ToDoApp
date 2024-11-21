using Data.Handlers;
using Data.Models;
using LogHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.Notifiers;

namespace ToDoApplication.Handlers
{
    internal static class ValueHandler
    {
        internal static bool IsCorrect<T>(T entity)
        {
            var result = false;
            switch (entity)
            {
                case User:
                    return IsUserCorrect((entity as User)!);
                case Objective:
                    return IsObjectiveCorrect((entity as Objective)!);
            }
            return result;
        }
        private static bool IsUserCorrect(User user)
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
        private static bool IsObjectiveCorrect(Objective objective)
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
