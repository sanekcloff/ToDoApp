using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        public User() { }

        // Скрытый конструктор
        private User(string lastName, string firstName, string middleName, string login, string password) 
        {
            Id = Guid.NewGuid();
            Lastname = lastName;
            Firstname = firstName;
            Middlename = middleName;
            Login = login;
            IsDeleted = false;
            Password = password;
        }

        // Идентификатор
        public Guid Id { get; set; }

        // Свойства
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Middlename { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Обратные навигационные свойства
        public virtual ICollection<Objective> CreatedObjectives { get; set; } = null!;
        public virtual ICollection<Objective> AssignedObjectives { get; set; } = null!;

        // Условные свойства
        public bool IsDeleted { get; set; }

        // Форматирование
        [NotMapped]
        public string Fullname => $"{Lastname} {Firstname} {Middlename}";
        public string sFullname => $"{Lastname} {Firstname[0]}. {Middlename[0]}.";

        // Метод создания экземляра
        public static User Create(string lastName, string firstName, string middleName, string login, string password)
        {
            return new(lastName, firstName, middleName, login, password);
        }
    }
}
