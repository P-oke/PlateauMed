using PlateauMedTask.Application.DTOs.StudentDTOs;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ResultModel<bool>> CreateStudent(CreateStudentDto model); 
        Task<ResultModel<bool>> UpdateStudent(Guid studentId, UpdateStudentDto model);
        Task<ResultModel<StudentDto>> GetAStudent(Guid studentId);
        Task<ResultModel<List<StudentDto>>> GetAllStudents();
        Task<ResultModel<PaginatedList<StudentDto>>> GetAllStudentsPaginated(BaseSearchViewModel model); 

    }
}
