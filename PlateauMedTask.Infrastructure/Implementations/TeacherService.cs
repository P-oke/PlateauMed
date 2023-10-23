using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// The teacher service class
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly UserManager<User> _userManager;

        private readonly ApplicationDbContext _dbContext;

        public TeacherService
        (
            UserManager<User> userManager,
            ApplicationDbContext dbContext
        )
        {
            _userManager = userManager;
            _dbContext = dbContext;  
        }

        public async Task<ResultModel<bool>> CreateTeacher(CreateTeacherDto model) 
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

            var teacher = new Teacher
            {
                TeacherNumber = model.TeacherNumber,
                Salary = model.Salary,
                Title = model.Title,
                CreatorUserId = user.Id,
            };

            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            return new ResultModel<bool>(true, "Teacher created successfully", ApiResponseCode.CREATED);

        }

        public async Task<ResultModel<List<TeacherDto>>> GetAllTeachers()
        {
            var teachers = await _dbContext.Teachers
                            .Include(x => x.User)
                            .OrderByDescending(x=> x.CreatedOn)
                            .ToListAsync();

            var teachersDto = teachers.Select(x=> (TeacherDto)x).ToList();

            return new ResultModel<List<TeacherDto>>(teachersDto, $"Successfully found {teachersDto.Count} teacher(s)");
         
        }

        public async Task<ResultModel<PaginatedList<TeacherDto>>> GetAllTeachersPaginated(BaseSearchViewModel model)
        {
            var query = _dbContext.Teachers
                            .Include(x => x.User)
                            .AsQueryable();

            var pagedNotifications = await query.OrderByDescending(x => x.CreatedOn).PaginateAsync(model.PageIndex, model.PageSize);

            var data = pagedNotifications.Select(x => (TeacherDto)x).ToList();

            return new ResultModel<PaginatedList<TeacherDto>>(new PaginatedList<TeacherDto>(data, model.PageIndex, model.PageSize, query.Count()), $"FOUND {data.Count} NOTIFICATIONS");
        }

        public async Task<ResultModel<TeacherDto>> GetATeacher(Guid teacherId)
        {
            var teacher = await _dbContext.Teachers
                                 .Where(x => x.Id == teacherId)
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync();

            if (teacher is null)
                return new ResultModel<TeacherDto>("Teacher doesn't exist", ApiResponseCode.NOT_FOUND);

            TeacherDto teacherDto = teacher;

            return new ResultModel<TeacherDto>(teacherDto, "Successfully retrieved");
        }

        public async Task<ResultModel<bool>> UpdateTeacher(Guid teacherId, UpdateTeacherDto model)
        {
            var teacher = await _dbContext.Teachers
                                 .Where(x => x.Id == teacherId)
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync();

            if (teacher is null)
                return new ResultModel<bool>("Teacher doesn't exist", ApiResponseCode.NOT_FOUND);

            teacher.User.FirstName = string.IsNullOrWhiteSpace(model.Name) ? teacher.User.FirstName : model.Name;
            teacher.User.LastName = string.IsNullOrWhiteSpace(model.Surname) ? teacher.User.LastName : model.Surname;
            teacher.User.NationalIdNumber = string.IsNullOrWhiteSpace(model.NationalIdNumber) ? teacher.User.NationalIdNumber : model.NationalIdNumber;
            teacher.User.DateOfBirth = model.DateOfBirth;
            teacher.User.LastModifierUserId = teacher.User.Id;
            teacher.User.LastModificationTime = DateTime.UtcNow;
            teacher.TeacherNumber = string.IsNullOrWhiteSpace(model.TeacherNumber)? teacher.TeacherNumber : model.TeacherNumber;
            teacher.Salary = model.Salary;
            teacher.Title = model.Title;
            teacher.ModifiedBy = teacher.User.Id;
            teacher.ModifiedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(teacher.User);
            _dbContext.Update(teacher);
            await _dbContext.SaveChangesAsync();

            return new ResultModel<bool>(true, "Successfully updated the teacher information");
        }
    }
}
