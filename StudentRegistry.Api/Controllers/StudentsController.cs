using Microsoft.AspNetCore.Mvc;
using StudentRegistry.Services.Students;
using StudentRegistry.Services.Students.Models;
using System.Net;

namespace StudentRegistry.Api.Controllers
{
    /// <summary>
    /// Students controller with authentication.
    /// </summary>
    [Route("students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsController"/> class.
        /// Students controller constructor.
        /// </summary>
        /// <param name="studentsService">students service.</param>
        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        /// <summary>Create a new student.</summary>
        /// <param name="studentInput">Student data input.</param>
        /// <response code ="201">Returns created.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Student.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(StudentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentInput studentInput)
        {
            return Created("students", await _studentsService.AddStudentAsync(studentInput));
        }

        /// <summary>Returns all existing students.</summary>
        /// <response code ="200">Returns all students.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Students.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<StudentResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentsService.GetAllStudentsAsync());
        }

        /// <summary>Returns an existing student.</summary>
        /// <response code ="200">Returns student.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Student.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(StudentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            return Ok(await _studentsService.GetStudentByIdAsync(id));
        }

        /// <summary>Update an existing student.</summary>
        /// <response code ="200">Returns updated student.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <param name="id">id student to update.</param>
        /// <param name="studentInput">Student data input.</param>
        /// <remarks>Student.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(StudentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentInput studentInput)
        {
            return Ok(await _studentsService.UpdateStudentAsync(id, studentInput));
        }

        /// <summary>Remove Student.</summary>
        /// <response code ="200">Returns Ok.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="404">Returns a KeyNotFound by Student id.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <param name="id">id student to delete.</param>
        /// <remarks>Student.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(StudentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentsService.DeleteStudentAsync(id);

            return Ok();
        }

        /// <summary>Return an excel report of students.</summary>
        /// <param name="createDate">Create date to filter students.</param>
        /// <response code ="200">Return excel report of students.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Students.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("excel")]
        public async Task<FileResult> GetStudentsExcelReport(DateTime? createDate)
        {
            var studentsReport = await _studentsService.GetStudentsExcelReportAsync(createDate);

            return File(studentsReport, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "students.xlsx");
        }
    }
}
