using Data.Context;
using Data.Models;
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
        public static void AddUser(string lastName, string firstName, string middleName, string login, string password)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Lastname = lastName,
                Firstname = firstName,
                Middlename = middleName,
                Login = login,
                IsDeleted = false,
                Password = password
            };
            try
            {
                DbWorker.AbstractContext.Users.Add(user);
                DbWorker.AbstractContext.SaveChanges();
                Debug.WriteLine($"Пользователь добавлен! {DbWorker.AbstractContext.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void UpdateUser(User user, string lastName, string firstName, string middleName, string login, string password)
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
                Debug.WriteLine($"Пользователь обновлён! {DbWorker.AbstractContext.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void DeleteUser(User user)
        {
            try
            {
                DbWorker.AbstractContext.Users.Remove(user);
                DbWorker.AbstractContext.SaveChanges();
                Debug.WriteLine($"Пользователь удалён! {DbWorker.AbstractContext.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void Hide(User user)
        {
            user.IsDeleted = true;
            DbWorker.AbstractContext.Users.Update(user);
            DbWorker.AbstractContext.SaveChanges();
            Debug.WriteLine($"Пользователь скрыт! {DbWorker.AbstractContext.GetType().Name}");
        }
    }
}
