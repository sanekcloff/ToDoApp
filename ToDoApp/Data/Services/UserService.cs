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
    public static class UserService
    {
        public static User? Find(string login, string password, AbstractContext context)
        {
            return context.Users.Where(u => u.Password == password && u.Login == login).FirstOrDefault();
        }
        public static void AddUser(string lastName, string firstName, string middleName, string login, string password, AbstractContext context)
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
                context.Users.Add(user);
                context.SaveChanges();
                Debug.WriteLine($"Пользователь добавлен! {context.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void UpdateUser(User user, string lastName, string firstName, string middleName, string login, string password, AbstractContext context)
        {
            user.Lastname = lastName;
            user.Firstname = firstName;
            user.Middlename = middleName;
            user.Login = login;
            user.Password = password;
            try
            {
                context.Users.Update(user);
                context.SaveChanges();
                Debug.WriteLine($"Пользователь обновлён! {context.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void DeleteUser(User user, AbstractContext context)
        {
            try
            {
                context.Users.Remove(user);
                context.SaveChanges();
                Debug.WriteLine($"Пользователь удалён! {context.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static void Hide(User user, AbstractContext context)
        {
            user.IsDeleted = true;
            context.Users.Update(user);
            context.SaveChanges();
            Debug.WriteLine($"Пользователь скрыт! {context.GetType().Name}");
        }
    }
}
