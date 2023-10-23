using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateauMedTask.Application.DTOs.StudentDTOs;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.Interfaces;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.Utils;

namespace PlateauMedTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;

        }

        /// <summary>
        /// CREATE A STUDENT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto model)
        {
            try
            {
                var result = await _studentService.CreateStudent(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A STUDENT
        /// </summary>
        /// <returns></returns>
        [HttpGet("{studentId}")]
        [ProducesResponseType(typeof(ResultModel<StudentDto>), 200)]
        public async Task<IActionResult> GetAStudent(Guid studentId)
        {
            try
            {
                var result = await _studentService.GetAStudent(studentId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// UPDATE A STUDENT
        /// </summary>
        /// <returns></returns>
        [HttpPut("{teacherId}")]
        [ProducesResponseType(typeof(ResultModel<StudentDto>), 200)]
        public async Task<IActionResult> UpdateStudent(Guid studentId, UpdateStudentDto model)
        {
            try
            {
                var result = await _studentService.UpdateStudent(studentId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL STUDENTS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<List<StudentDto>>), 200)]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var result = await _studentService.GetAllStudents();

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }

        }

        /// <summary>
        /// GET ALL STUDENTS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<StudentDto>>), 200)]
        public async Task<IActionResult> GetAllStudentsPaginated([FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _studentService.GetAllStudentsPaginated(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount);

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }



    }
}
