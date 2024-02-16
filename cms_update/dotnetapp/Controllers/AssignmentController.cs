// AssignmentController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAllAssignments()
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignments();
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{assignmentId}")]
        public async Task<ActionResult<Assignment>> GetAssignmentById(long assignmentId)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentById(assignmentId);

                if (assignment == null)
                {
                    return NotFound(new { message = "Cannot find the assignment" });
                }

                return Ok(assignment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignmentsByUserId(long userId)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByUserId(userId);
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAssignment([FromBody] Assignment newAssignment)
        {
            try
            {
                var addedAssignment = await _assignmentService.AddAssignment(newAssignment);
                return CreatedAtAction(nameof(GetAssignmentById), new { assignmentId = addedAssignment.AssignmentId }, addedAssignment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{assignmentId}")]
        public async Task<ActionResult> UpdateAssignment(long assignmentId, [FromBody] Assignment updatedAssignment)
        {
            try
            {
                var success = await _assignmentService.UpdateAssignment(assignmentId, updatedAssignment);
                if (success)
                    return Ok(updatedAssignment);
                else
                    return NotFound(new { message = "Cannot find the assignment" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{assignmentId}")]
        public async Task<ActionResult> DeleteAssignment(long assignmentId)
        {
            try
            {
                var success = await _assignmentService.DeleteAssignment(assignmentId);
                if (success)
                    return Ok(new { message = "Assignment deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the assignment" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
