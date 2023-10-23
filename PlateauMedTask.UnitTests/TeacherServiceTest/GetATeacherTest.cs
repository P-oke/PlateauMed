using Microsoft.EntityFrameworkCore;
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
    public class GetATeacherTest
    {
        private readonly TeacherServiceFactory _fac;

        public GetATeacherTest()
        {
            _fac = new TeacherServiceFactory();
        }

        [Fact]
        public async Task GetATeacher_ShouldWork()
        {
            //Arrange

            var teacher = new Teacher
            {
                TeacherNumber = "1234",
                Salary = 3500,
                Title = Title.Mr,
                User = new User()              
            };

            await _fac.Context.Teachers.AddAsync(teacher);
            await _fac.Context.SaveChangesAsync();


            //Act

            var result = await _fac.TeacherService.GetATeacher(teacher.Id);

            //Assert
            Assert.False(result.HasError);

        }

        [Fact]
        public async Task GetATeacher_ShouldReturnError_WhenTeacherDoesnotExist() 
        {
            //Arrange
            var teacherId = Guid.NewGuid();

            //Act
            var result = await _fac.TeacherService.GetATeacher(teacherId);

            //Assert
            Assert.True(result.HasError);

        }

    }
}
