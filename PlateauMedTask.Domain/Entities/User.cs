using Microsoft.AspNetCore.Identity;
using PlateauMedTask.Domain.Entities.Common;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities
{
    public class User : IdentityUser<Guid>, ICreationAudited<Guid>, IModificationAudited<Guid?>, IDeletionAudited<Guid?>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdNumber { get; set; } 
        public UserType UserType { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DateOfBirth { get; set; } 

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public Guid CreatorUserId { get; set; }
        public Guid? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }

    public class UserClaim : IdentityUserClaim<Guid> { }

    public class UserRole : IdentityUserRole<Guid> { }

    public class UserLogin : IdentityUserLogin<Guid> { }
   
    public class RoleClaim : IdentityRoleClaim<Guid> { }

    public class UserToken : IdentityUserToken<Guid> { }
}
