using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.DTOs.TeacherDTOs;

namespace PlateauMedTask.UnitTests.TeacherServiceTest
{
    public class UpdateTeacherTest
    {
        private readonly TeacherServiceFactory _fac;

        public UpdateTeacherTest()
        {
            _fac = new TeacherServiceFactory();
        }

        [Fact]
        public async Task UpdateTeacher_ShouldWork()
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
            var result = await _fac.TeacherService.UpdateTeacher(teacher.Id, new UpdateTeacherDto());

            //Assert
            Assert.False(result.HasError);
        }


        [Fact]
        public async Task UpdateTeacher_ShouldReturnError_WhenTeacherDoesnotExist()
        {
            //Arrange
            var teacherId = Guid.NewGuid();

            //Act
            var result = await _fac.TeacherService.UpdateTeacher(teacherId, new UpdateTeacherDto());

            //Assert
            Assert.True(result.HasError);

        }
    }
}
