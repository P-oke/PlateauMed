using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlateauMedTask.Application.DTOs.TeacherDTOs;
using PlateauMedTask.Application.Interfaces;
using PlateauMedTask.Application.Models;
using PlateauMedTask.Application.Utils;
using PlateauMedTask.Domain.Entities;

namespace PlateauMedTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService; 

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;     
        }

        /// <summary>
        /// CREATE A TEACHER
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherDto model)
        {
            try
            {
                var result = await _teacherService.CreateTeacher(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A TEACHER
        /// </summary>
        /// <returns></returns>
        [HttpGet("{teacherId}")]
        [ProducesResponseType(typeof(ResultModel<TeacherDto>), 200)]
        public async Task<IActionResult> GetATeacher(Guid teacherId)
        {
            try
            {
                var result = await _teacherService.GetATeacher(teacherId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// UPDATE A TEACHER
        /// </summary>
        /// <returns></returns>
        [HttpPut("{teacherId}")]
        [ProducesResponseType(typeof(ResultModel<TeacherDto>), 200)]
        public async Task<IActionResult> UpdateTeacher(Guid teacherId, UpdateTeacherDto model)
        {
            try
            {
                var result = await _teacherService.UpdateTeacher(teacherId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL TEACHERS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<List<TeacherDto>>), 200)]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var result = await _teacherService.GetAllTeachers();

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        
        }

        /// <summary>
        /// GET ALL TEACHERS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<TeacherDto>>), 200)]
        public async Task<IActionResult> GetAllTeachersPaginated([FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _teacherService.GetAllTeachersPaginated(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount);

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


    }
}
