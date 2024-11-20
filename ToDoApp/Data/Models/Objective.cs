using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Objective
    {
        public Objective() { }
        // Скрытый конструктор
        private Objective(User creator, User assigner, string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreateDate = DateTime.Now;
            Creator = creator;
            Assignee = assigner;
            IsDeleted = false;
            IsExecuted = false;
            ExecuteDate = null;
        }

        // Идентификатор
        public Guid Id { get; set; }

        // Свойства 
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExecuteDate { get; set; }

        // Навигационные свойства
        public Guid CreatorId { get; set; }
        public Guid AssigneeId { get; set; }
        public virtual User Creator { get; set; } = null!;
        public virtual User Assignee { get; set; } = null!;

        // Условные свойсвтва
        public bool IsExecuted { get; set; }
        public bool IsDeleted { get; set; }

        //Форматирование
        [NotMapped]
        public string ForamatedCreateDate => CreateDate.ToString("d");
        public string ForamatedExecuteDate => ExecuteDate!.Value.ToString("d");

        // Метод создания экземляра
        public static Objective Create(User creator, User assigner, string title, string description)
        {
            return new(creator, assigner, title, description);
        }
    }
}
