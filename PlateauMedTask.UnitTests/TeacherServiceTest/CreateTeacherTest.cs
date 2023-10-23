using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.UnitTests.TeacherServiceTest
{
    public class CreateTeacherTest
    {
        private readonly TeacherServiceFactory _fac;


        public CreateTeacherTest()
        {
            _fac = new TeacherServiceFactory();
        }

        [Fact]
        public async Task CreateTeacher_ShouldWork()
        {
            //Arrange
            var data = new CreateTeacherDto
            {
                Email = "test@yopmail.com",
                Name = "Mary",
                Surname = "Jane",
                DateOfBirth = new DateTime(2000, 7, 5, 16, 23, 42, DateTimeKind.Utc),
                UserType = UserType.Teacher,
                NationalIdNumber = "123456",
                Salary = 3500,
                Title = Title.Mr

            };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);


            //Act
            var result = await _fac.TeacherService.CreateTeacher(data);

            //Assert
            Assert.False(result.HasError);

        }

        [Fact]
        public async Task CreateTeacher_ShouldReturnError_WhenUserExist()
        {
            //Arrange

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Failed());

            //Act
            var result = await _fac.TeacherService.CreateTeacher(new CreateTeacherDto());

            //Assert
            Assert.True(result.HasError);

        }

        [Fact]
        public async Task CreateTeacher_ShouldReturnError_WhenUserEmailExist()
        {
            //Arrange

            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            //Act
            var result = await _fac.TeacherService.CreateTeacher(new CreateTeacherDto());

            //Assert
            Assert.True(result.HasError);

        }

    }
}
