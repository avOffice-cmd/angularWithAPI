using Application.CoursesCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Application.StudentsCQRS.Commands;
using Application.StudentsCQRS.Queries;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class StudentController : APIControllerBase
    {

        private readonly ILogger<StudentController> logger;
        private readonly IValidator<AddStudentDTO> addStudentValidator;

        public IConfiguration configuration { get; }
        public StudentController( IConfiguration _configuration,ILogger<StudentController> logger, IValidator<AddStudentDTO> _addStudentValidator)
        {
            addStudentValidator = _addStudentValidator;
            configuration = _configuration;
            this.logger = logger;
        }

        private ActionResult HandleValidationResult(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }
            return null; // Return null if validation is successful
        }


        /// <summary>
        /// Used to get all students from the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAllStudents()
        {
            var allStudents = await Mediator.Send(new GetAllStudents { });
            return Ok(allStudents);
        }


        /// <summary>
        /// Used to fetch students by id
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentByID")]
        public async Task<ActionResult> GetStudentByID(int _studentId)
        {
            var response = await Mediator.Send(new GetStudentByID { StdId = _studentId });
            return Ok(response);
        }

        /// <summary>
        /// Used to insert student into table
        /// </summary>
        /// <param name="_addStudentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddStudent([FromBody] AddStudentDTO _addStudentDTO)
        {

            //var validationResult = addCourseValidator.Validate(_addCourseDTO);
            var validationResult = await addStudentValidator.ValidateAsync(_addStudentDTO);

            //This repeated code is added to a function above 'HandleValidationResult'
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }

            //var result = HandleValidationResult(validationResult);
            //if (result != null) return result;

            var response = await Mediator.Send(new AddStudent { _addStudentDTO = _addStudentDTO });
            return Ok(response);
        }

     


        /// <summary>
        /// Used to delete student from table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteStudentByIDAsync(int id)
        {
            var response = await Mediator.Send(new DeleteStudent { StudentID = id });
            return Ok(response);
        }


        /// <summary>
        /// Used to update student in table
        /// </summary>
        /// <param name="_updateStudentDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentDTO _updateStudentDTO)
        {
            var response = await Mediator.Send(new UpdateStudent { UpdateStudentDTO = _updateStudentDTO });
            return Ok(response);
        }

        // LOGGER //
        [HttpGet]
        [Route("logger")]
        public ActionResult Index()
        {

            // all are log level messages
            logger.LogTrace("Log msg from trace");
            logger.LogInformation("log msg from loginformation");
            logger.LogDebug("log msg from log debug");
            logger.LogWarning("log msg from logwarning");
            logger.LogError("log msg from logError");
            logger.LogCritical("log msg from logcritical");


            return Ok();
        }
    }
}
