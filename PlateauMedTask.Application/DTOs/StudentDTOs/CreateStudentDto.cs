using PlateauMedTask.Application.Helpers;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.DTOs.StudentDTOs
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "National identification number is required")]
        public string NationalIdNumber { get; set; }
        [Required(ErrorMessage = "Student number is required")]
        public string StudentNumber { get; set; }
        public UserType UserType { get; set; } = UserType.Student;
        [ValidateAge(22, ErrorMessage = "You must be at least 22 years old.")]
        public DateTime DateOfBirth { get; set; }
      
    }
}
