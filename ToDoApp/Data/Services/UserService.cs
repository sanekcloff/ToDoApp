using Data.Context;
using Data.Models;
using LogHandler;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    // Класс для работы с User
    public static class UserService
    {
        // Получить пользователей с их задачами
        public static ICollection<User> GetUsers()
        {
            return DbWorker.AbstractContext.Users
                .Include(u => u.AssignedObjectives)
                .Include(u => u.CreatedObjectives)
                .ToList();
        }

        // Найти пользователя по логину и паролю
        public static User? Find(string login, string password)
        {
            return DbWorker.AbstractContext.Users.Where(u => u.Password == password && u.Login == login).FirstOrDefault();
        }

        // Добавление
        public async static Task<bool> Add(User user)
        {
            try
            {
                if (Find(user.Login, user.Password) != null)
                    throw new Exception("Ошибка! Пользователь уже существует!");
                else
                {
                    DbWorker.AbstractContext.Users.Add(user);
                    DbWorker.AbstractContext.SaveChanges();
                    await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) добавлен!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
                return false;
            }
        }

        // Обновить
        public async static void Update(User user, string lastName, string firstName, string middleName, string login, string password)
        {
            user.Lastname = lastName;
            user.Firstname = firstName;
            user.Middlename = middleName;
            user.Login = login;
            user.Password = password;
            try
            {
                DbWorker.AbstractContext.Users.Update(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) обновлён!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }

        // Удалить
        public async static void Delete(User user)
        {
            try
            {
                DbWorker.AbstractContext.Users.Remove(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) удалён!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }

        // Скрыть
        public async static void Hide(User user)
        {
            if (user.IsDeleted == false)
            {
                user.IsDeleted = true;
                DbWorker.AbstractContext.Users.Update(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) скрыт!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) уже скрыт!");
            }
        }
        
        // Сделать видимым
        public async static void Show(User user)
        {
            if (user.IsDeleted == true)
            {
                user.IsDeleted = false;
                DbWorker.AbstractContext.Users.Update(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) теперь доступен!");
            }
            else
            {
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь ({user.Fullname}) уже доступен!");
            }
        }
    }
}
