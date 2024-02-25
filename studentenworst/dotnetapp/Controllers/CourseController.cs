using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    // [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("api/course")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("swagger")]
        public IActionResult Swagger()
        {
            return Ok();
        }



        [HttpGet("api/course/{courseId}")]
        public async Task<ActionResult<Course>> GetCourseById(int courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);

                if (course == null)
                {
                    return NotFound(new { message = "Cannot find the course" });
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost("api/course")]
        public async Task<ActionResult> AddCourse([FromBody] Course newCourse)
        {
            try
            {
                var addedCourse = await _courseService.AddCourse(newCourse);
                return CreatedAtAction(nameof(GetCourseById), new { courseId = addedCourse.CourseID }, addedCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
        [HttpPut("api/course{courseId}")]
        public async Task<ActionResult> UpdateCourse(int courseId, [FromBody] Course updatedCourse)
        {
            try
            {
                var success = await _courseService.UpdateCourse(courseId, updatedCourse);

                if (success)
                    return Ok(new { message = "Course updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the course" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("{courseId}")]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            try
            {
                var success = await _courseService.DeleteCourse(courseId);

                if (success)
                    return Ok(new { message = "Course deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the course" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
