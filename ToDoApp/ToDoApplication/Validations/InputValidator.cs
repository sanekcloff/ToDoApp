using Data.Handlers;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoApplication.Notifiers;

namespace ToDoApplication.Validations
{
    internal static class InputValidator
    {
        internal static bool UserValidation(User user)
        {
            if(InputHandler.Handle(user))
            {
                MessageNotifier.Information($"Пользователь {user.Fullname} добавлен!");
            }
            else
            {
                MessageNotifier.Error($"Не удалось добавить пользователя!");
            }
        }
        internal static bool ObjectiveValidation(Objective objective)
        {
            return false;
        }
    }
}
