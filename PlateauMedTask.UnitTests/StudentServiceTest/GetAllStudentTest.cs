using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Domain.Entities.Enum;
using PlateauMedTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.DTOs.StudentDTOs;

namespace PlateauMedTask.UnitTests.StudentServiceTest
{
    public class GetAllStudentTest
    {
        private readonly StudentServiceFactory _fac;


        public GetAllStudentTest()
        {
            _fac = new StudentServiceFactory();

        }

        [Fact]
        public async Task GetAllStudents_ShouldWork()
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
            var result = await _fac.StudentService.GetAllStudents();

            //Assert
            Assert.True(result.Data.Count >= 1);
        }


        [Fact]
        public async Task GetAllStudent_Paginated_ShouldWork()
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

            var result = await _fac.StudentService.GetAllStudentsPaginated(new BaseSearchViewModel());

            //Assert
            Assert.True(result.Data.Count >= 1);
            Assert.IsType<PaginatedList<StudentDto>>(result.Data);
        }
    }
}
