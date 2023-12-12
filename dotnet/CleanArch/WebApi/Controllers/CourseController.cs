using Application.CoursesCQRS.Commands;
using Application.CoursesCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Application.StudentsCQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CourseController : APIControllerBase
    {

        private readonly ILogger<CourseController> logger;

        public IConfiguration configuration { get; }
        public CourseController(IConfiguration _configuration, ILogger<CourseController> logger)
        {

            configuration = _configuration;
            this.logger = logger;
        }

        /// <summary>
        /// Used to get all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAllCourses()
        {
            var gotAllCourses = await Mediator.Send(new GetAllCourses { });
            return Ok(gotAllCourses);
        }


        /// <summary>
        /// Used to fetch Courses by id
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCourseByID")]
        public async Task<ActionResult> GetCourseByID(int courseID)
        {
            var gotCourse = await Mediator.Send(new GetCourseByID { CourseID = courseID });
            return Ok(gotCourse);
        }


        /// <summary>
        /// Used to insert course into table
        /// </summary>
        /// <param name="_addCourseDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCourse([FromBody] AddCourseDTO _addCourseDTO)
        {
            var gotCourse = await Mediator.Send(new AddCourse { _addCourseDTO = _addCourseDTO });
            return Ok(gotCourse);
        }


       
        /// <summary>
        /// Used to delete course from table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteCourseByID(int id)
        {
            var reponse = await Mediator.Send(new DeleteCourse { CourseID = id });
            return Ok(reponse);
        }


        /// <summary>
        /// Used to update course in the table
        /// </summary>
        /// <param name="updateCourseDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateCourse([FromBody] UpdateCourseDTO updateCourseDTO)
        {
            var response = await Mediator.Send(new UpdateCourse { UpdateCourseDTO = updateCourseDTO });
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
