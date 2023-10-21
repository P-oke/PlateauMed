using Microsoft.AspNetCore.Identity;
using PlateauMedTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities
{
    public class Role : IdentityRole<Guid>, ICreationAudited<Guid>, IModificationAudited<Guid?>, IDeletionAudited<Guid?>
    {
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDefaultRole { get; set; }
        public Guid CreatorUserId { get; set; }
        public Guid? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
