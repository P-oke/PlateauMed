using PlateauMedTask.Domain.Entities.Common;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities
{
    public class Teacher : BaseEntity<Guid>
    {
        public Title Title { get; set; }
        public string TeacherNumber { get; set; }
        public decimal Salary { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; } 
    }
}
    