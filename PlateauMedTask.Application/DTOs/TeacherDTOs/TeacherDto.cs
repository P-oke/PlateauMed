using PlateauMedTask.Application.DTOs.UserDTOs;
using PlateauMedTask.Application.Utils;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.DTOs.TeacherDTOs
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TeacherNumber { get; set; }
        public decimal Salary { get; set; }
        public UserDto UserDto { get; set; }
        public DateTime CreatedOn { get; set; }



        public static implicit operator TeacherDto(Teacher model)
        {
            return model is null ? null : new TeacherDto
            {
                Id = model.Id,
                Title = model.Title.GetDescription(),
                TeacherNumber = model.TeacherNumber,
                Salary = model.Salary,
                UserDto = model.User,
                CreatedOn = model.CreatedOn

            };

        }

    }
}
