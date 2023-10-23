using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.DTOs.TeacherDTOs;

namespace PlateauMedTask.UnitTests.TeacherServiceTest
{
    public class GetAllTeacherTest
    {
        private readonly TeacherServiceFactory _fac;

        public GetAllTeacherTest()
        {
            _fac = new TeacherServiceFactory();

        }

        [Fact]
        public async Task GetAllTeachers()
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

            var result = await _fac.TeacherService.GetAllTeachers();

            //Assert
            Assert.True(result.Data.Count >= 1);
        }


        [Fact]
        public async Task GetAllTeachers_Paginated()
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

            var result = await _fac.TeacherService.GetAllTeachersPaginated(new BaseSearchViewModel());

            //Assert
            Assert.True(result.Data.Count >= 1);
            Assert.IsType<PaginatedList<TeacherDto>>(result.Data);
        }
    }
}
