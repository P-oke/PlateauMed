using PlateauMedTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities
{
    public class Student : BaseEntity<Guid>
    {
        public string StudentNumber { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
