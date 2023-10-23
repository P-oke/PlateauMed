using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.DTOs.UserDTOs;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.DTOs.StudentDTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string StudentNumber { get; set; }
        public decimal Salary { get; set; }
        public UserDto UserDto { get; set; }
        public DateTime CreatedOn { get; set; }


        public static implicit operator StudentDto(Student model)
        {
            return model is null ? null : new StudentDto
            {
                Id = model.Id,
                StudentNumber = model.StudentNumber,
                UserDto = model.User,
                CreatedOn = model.CreatedOn

            };

        }

    }
}
