using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<ResultModel<bool>> CreateTeacher(CreateTeacherDto model); 
        Task<ResultModel<bool>> UpdateTeacher(Guid teacherId, UpdateTeacherDto model); 
        Task<ResultModel<TeacherDto>> GetATeacher(Guid teacherId);   
        Task<ResultModel<List<TeacherDto>>> GetAllTeachers();
        Task<ResultModel<PaginatedList<TeacherDto>>> GetAllTeachersPaginated(BaseSearchViewModel model);

    }
}
