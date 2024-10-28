using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Objective
    {
        // Идентификатор
        public Guid Id { get; set; }
        // Свойства 
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExecuteDate { get; set; }
        // Навигационные свойства
        public Guid CreatorId { get; set; }
        public Guid AssigneeId { get; set; }
        public virtual User Creator { get; set; } = null!;
        public virtual User Assignee { get; set; } = null!;

        public bool IsExecuted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
