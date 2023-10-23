using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PlateauMedTask.API.Controllers;
using PlateauMedTask.Application.DTOs.StudentDTOs;
using PlateauMedTask.Application.Interfaces;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Infrastructure.Context;
using PlateauMedTask.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.UnitTests.ControllerTest
{
    public class StudentControllerTest
    {
        public readonly ApplicationDbContext Context;
        public Mock<UserManager<User>> UserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
            null, null, null, null, null, null, null, null);


        public StudentControllerTest()
        {
            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "PlateauMed")
             .Options);

            var studentService = new StudentService(UserManager.Object, Context);
            StudentController = new StudentController(studentService);
        }

        public StudentController StudentController { get; set; }


        [Fact]
        public async Task GetStudent_ShouldWork()
        {
            //Arrange

            var student = new Student
            {
                StudentNumber = "1234",
                User = new User()
            };

            await Context.Students.AddAsync(student);
            await Context.SaveChangesAsync();

            // Act

            var result = await StudentController.GetAStudent(student.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
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

            UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // Act

            var result = await StudentController.CreateStudent(data) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
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

            await Context.Students.AddAsync(student);
            await Context.SaveChangesAsync();

            // Act

            var result = await StudentController.UpdateStudent(student.Id, new UpdateStudentDto()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllStudent_ShouldWork()
        {
            //Arrange

            var student = new Student
            {
                StudentNumber = "1234",
                User = new User()
            };

            await Context.Students.AddAsync(student);
            await Context.SaveChangesAsync();

            // Act

            var result = await StudentController.GetAllStudents() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task GetAllStudentPaginated_ShouldWork() 
        {
            //Arrange

            var student = new Student
            {
                StudentNumber = "1234",
                User = new User()
            };

            await Context.Students.AddAsync(student);
            await Context.SaveChangesAsync();

            // Act

            var result = await StudentController.GetAllStudentsPaginated(new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


    }
}
