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
    public static class UserService
    {
        public static ICollection<User> GetUsers()
        {
            return DbWorker.AbstractContext.Users
                .Include(u => u.AssignedObjectives)
                .Include(u => u.CreatedObjectives)
                .ToList();
        }
        public static User? Find(string login, string password)
        {
            return DbWorker.AbstractContext.Users.Where(u => u.Password == password && u.Login == login).FirstOrDefault();
        }
        public async static void Add(User user)
        {
            try
            {
                if (Find(user.Login, user.Password) != null)
                    throw new Exception("Ошибка! Пользователь уже существует!");
                DbWorker.AbstractContext.Users.Add(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь добавлен!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }
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
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь обновлён!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }
        public async static void Delete(User user)
        {
            try
            {
                DbWorker.AbstractContext.Users.Remove(user);
                DbWorker.AbstractContext.SaveChanges();
                await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь удалён!");
            }
            catch (Exception ex)
            {
                await Logger.AddLogAsync(ex.Message);
            }
        }
        public async static void Hide(User user)
        {
            user.IsDeleted = true;
            DbWorker.AbstractContext.Users.Update(user);
            DbWorker.AbstractContext.SaveChanges();
            await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь скрыт!");
        }
        public async static void Show(User user)
        {
            user.IsDeleted = false;
            DbWorker.AbstractContext.Users.Update(user);
            DbWorker.AbstractContext.SaveChanges();
            await Logger.AddLogAsync($"{DbWorker.AbstractContext.GetType().Name} - Пользователь скрыт!");
        }
    }
}
