using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.DTOs.StudentDTOs;

namespace PlateauMedTask.UnitTests.StudentServiceTest
{
    public class UpdateStudentTest
    {
        private readonly StudentServiceFactory _fac;

        public UpdateStudentTest()
        {
            _fac = new StudentServiceFactory();

        }

        [Fact]
        public async Task UpdateStudent_ShouldWork()
        {
            //Arrange

            var student = new Student
            {
                StudentNumber = "1234",
                User = new User()
            };

            await _fac.Context.Students.AddAsync(student);
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.StudentService.UpdateStudent(student.Id, new UpdateStudentDto());

            //Assert
            Assert.False(result.HasError);
        }


        [Fact]
        public async Task UpdateStudent_ShouldReturnError_WhenStudentDoesnotExist()
        {
            //Arrange
            var studentId = Guid.NewGuid();

            //Act
            var result = await _fac.StudentService.UpdateStudent(studentId, new UpdateStudentDto());

            //Assert
            Assert.True(result.HasError);

        }
    }
}

