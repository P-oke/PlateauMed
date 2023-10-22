using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.DTOs.UserDTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalIdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; set; }


        public static implicit operator UserDto(User model)
        {
            return model is null ? null : new UserDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                NationalIdNumber = model.NationalIdNumber,
                DateOfBirth = model.DateOfBirth,
                CreatedOn = model.CreationTime
            };

        }
    }
}
