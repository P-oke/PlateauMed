using PlateauMedTask.Application.Interfaces;
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

    public class GetAStudentTest
    {
        private readonly StudentServiceFactory _fac;

        public GetAStudentTest()
        {
            _fac = new StudentServiceFactory(); 
        }


        [Fact]
        public async Task GetAStudent_ShouldWork()
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

            var result = await _fac.StudentService.GetAStudent(student.Id);

            //Assert
            Assert.False(result.HasError);

        }

        [Fact]
        public async Task GetAStudent_ShouldReturnError_WhenStudentDoesnotExist()
        {
            //Arrange
            var studentId = Guid.NewGuid();

            //Act
            var result = await _fac.StudentService.GetAStudent(studentId);

            //Assert
            Assert.True(result.HasError);

        }
    }
}
