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

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string Fullname => $"{Lastname} {Firstname[0]}. {Middlename[0]}.";
    }
}
