using Microsoft.AspNetCore.Identity;
using Moq;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.UnitTests.TeacherServiceTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.DTOs.StudentDTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlateauMedTask.UnitTests.StudentServiceTest
{
    public class CreateStudentTest
    {
        private readonly StudentServiceFactory _fac;

        public CreateStudentTest()
        {
            _fac = new StudentServiceFactory();  
        }

        [Fact]
        public async Task CreateStudent_ShouldWork()
        {
            //Arrange
            var data = new CreateStudentDto
            {
                Email = "student@yopmail.com",
                Name = "Mary",
                Surname = "Jane",
                DateOfBirth = new DateTime(2000, 7, 5, 16, 23, 42, DateTimeKind.Utc),
                UserType = UserType.Student,
                NationalIdNumber = "123456",
                StudentNumber = "12345"

            };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);


            //Act
            var result = await _fac.StudentService.CreateStudent(data);

            //Assert
            Assert.False(result.HasError);

        }

        [Fact]
        public async Task CreateStudent_ShouldReturnError_WhenUserExist()
        {
            //Arrange

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Failed());

            //Act
            var result = await _fac.StudentService.CreateStudent(new CreateStudentDto());

            //Assert
            Assert.True(result.HasError);

        }

        [Fact]
        public async Task CreateStudent_ShouldReturnError_WhenUserEmailExist()
        {
            //Arrange

            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            //Act
            var result = await _fac.StudentService.CreateStudent(new CreateStudentDto());

            //Assert
            Assert.True(result.HasError);

        }

    }
}
