using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PlateauMedTask.API.Controllers;
using PlateauMedTask.Application.DTOs.StudentDTOs;
using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Infrastructure.Context;
using PlateauMedTask.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.Models;

namespace PlateauMedTask.UnitTests.ControllerTest
{
    public class TeacherControllerTest
    {
        public readonly ApplicationDbContext Context;
        public Mock<UserManager<User>> UserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
            null, null, null, null, null, null, null, null);


        public TeacherControllerTest() 
        {
            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "PlateauMed")
             .Options);

            var teacherService = new TeacherService(UserManager.Object, Context);
            TeacherController = new TeacherController(teacherService);
        }

        public TeacherController TeacherController { get; set; }


        [Fact]
        public async Task GetTeacher_ShouldWork() 
        {
            //Arrange

            var teacher = new Teacher
            {
                TeacherNumber = "1234",
                Salary = 3500,
                Title = Title.Mr,
                User = new User()
            };

            await Context.Teachers.AddAsync(teacher);
            await Context.SaveChangesAsync();


            UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);


            // Act

            var result = await TeacherController.GetATeacher(teacher.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task CreateTeacher_ShouldWork()
        {
            //Arrange

            var teacher = new CreateTeacherDto
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

            UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act

            var result = await TeacherController.CreateTeacher(teacher) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task UpdateStudent_ShouldWork()
        {
            //Arrange

            var teacher = new Teacher
            {
                TeacherNumber = "1234",
                Salary = 3500,
                Title = Title.Mr,
                User = new User()
            };

            await Context.Teachers.AddAsync(teacher);
            await Context.SaveChangesAsync();

            // Act

            var result = await TeacherController.UpdateTeacher(teacher.Id, new UpdateTeacherDto()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllTeachers_ShouldWork()
        {
            //Arrange

            var teacher = new Teacher
            {
                TeacherNumber = "1234",
                Salary = 3500,
                Title = Title.Mr,
                User = new User()
            };

            await Context.Teachers.AddAsync(teacher);
            await Context.SaveChangesAsync();

            // Act

            var result = await TeacherController.GetAllTeachers() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task GetAllTeachersPaginated_ShouldWork()
        {
            //Arrange

            var teacher = new Teacher
            {
                TeacherNumber = "1234",
                Salary = 3500,
                Title = Title.Mr,
                User = new User()
            };

            await Context.Teachers.AddAsync(teacher);
            await Context.SaveChangesAsync();


            // Act

            var result = await TeacherController.GetAllTeachersPaginated(new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
