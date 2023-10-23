using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlateauMedTask.Application.DTOs.StudentDTOs;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.Interfaces;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.Models.Enums;
using PlateauMedTask.Application.Utils;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Infrastructure.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<User> _userManager;

        private readonly ApplicationDbContext _dbContext;

        public StudentService
        (
             UserManager<User> userManager,
            ApplicationDbContext dbContext
        )
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<ResultModel<bool>> CreateStudent(CreateStudentDto model)
        {
            var validateUser = await _userManager.FindByEmailAsync(model.Email);

            if (validateUser is not null)
                return new ResultModel<bool>("User email already exist", ApiResponseCode.INVALID_REQUEST);


            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.Name,
                LastName = model.Surname,
                DateOfBirth = model.DateOfBirth,
                UserType = model.UserType,
                NationalIdNumber = model.NationalIdNumber,
                CreationTime = DateTime.UtcNow
            };

            var createUserResponse = await _userManager.CreateAsync(user);

            if (!createUserResponse.Succeeded)
            {
                return new ResultModel<bool>(string.Join(",", createUserResponse.Errors.Select(x => x.Description)), ApiResponseCode.INVALID_REQUEST);
            }

            var student = new Student
            {
                UserId = user.Id,
                StudentNumber = model.StudentNumber,
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return new ResultModel<bool>(true, "Student Created Successfully", ApiResponseCode.CREATED);

        }

        public async Task<ResultModel<List<StudentDto>>> GetAllStudents()
        {
            var students = await _dbContext.Students
                                        .Include(x => x.User)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .ToListAsync();

            var teachersDto = students.Select(x => (StudentDto)x).ToList();

            return new ResultModel<List<StudentDto>>(teachersDto, $"Successfully found {teachersDto.Count} student(s)");
        }

        public async Task<ResultModel<PaginatedList<StudentDto>>> GetAllStudentsPaginated(BaseSearchViewModel model)
        {
            var query = _dbContext.Students
                           .Include(x => x.User)
                           .AsQueryable();

            var pagedNotifications = await query.OrderByDescending(x => x.CreatedOn).PaginateAsync(model.PageIndex, model.PageSize);

            var data = pagedNotifications.Select(x => (StudentDto)x).ToList();

            return new ResultModel<PaginatedList<StudentDto>>(new PaginatedList<StudentDto>(data, model.PageIndex, model.PageSize, query.Count()), $"Successfully found {data.Count} student(s)");
        }

        public async Task<ResultModel<StudentDto>> GetAStudent(Guid studentId)
        {
            var student = await _dbContext.Students
                                           .Where(x => x.Id == studentId)
                                           .Include(x => x.User)
                                           .FirstOrDefaultAsync();

            if (student is null)
                return new ResultModel<StudentDto>("Student doesn't exist", ApiResponseCode.NOT_FOUND);

            StudentDto studentDto = student;

            return new ResultModel<StudentDto>(studentDto, "Successfully retrieved");
        }

        public async Task<ResultModel<bool>> UpdateStudent(Guid studentId, UpdateStudentDto model)
        {
            var student = await _dbContext.Students
                                  .Where(x => x.Id == studentId)
                                  .Include(x => x.User)
                                  .FirstOrDefaultAsync();

            if (student is null)
                return new ResultModel<bool>("Student doesn't exist", ApiResponseCode.NOT_FOUND);

            student.User.FirstName = string.IsNullOrWhiteSpace(model.Name) ? student.User.FirstName : model.Name;
            student.User.LastName = string.IsNullOrWhiteSpace(model.Surname) ? student.User.LastName : model.Surname;
            student.User.NationalIdNumber = string.IsNullOrWhiteSpace(model.NationalIdNumber) ? student.User.NationalIdNumber : model.NationalIdNumber;
            student.User.DateOfBirth = model.DateOfBirth;
            student.User.LastModifierUserId = student.User.Id;
            student.User.LastModificationTime = DateTime.UtcNow;
            student.StudentNumber = string.IsNullOrWhiteSpace(model.StudentNumber) ? student.StudentNumber : model.StudentNumber;
            student.ModifiedBy = student.User.Id;
            student.ModifiedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(student.User);
            _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();

            return new ResultModel<bool>(true, "Successfully updated the student information");
        }
    }
}
