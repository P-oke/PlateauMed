using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PlateauMedTask.Application.Helpers;

namespace PlateauMedTask.Application.DTOs.TeacherDTOs
{
    public class CreateTeacherDto 
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } 
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public Title Title { get; set; }
        [Required(ErrorMessage = "National identification number is required")]
        public string NationalIdNumber { get; set; }
        public UserType UserType { get; set; } = UserType.Teacher;
        [ValidateAge(21, ErrorMessage = "You must be at least 21 years old.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Teacher's number is required")]
        public string TeacherNumber { get; set; }
        public decimal Salary { get; set; }

    }
}
