// AssignmentController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;

namespace dotnetapp.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _assignmentService;

        public AssignmentController(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }


       // [Authorize(Roles = "Admin")]
        // [Authorize(Roles = "Admin")]
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

        [Authorize]
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

   
        // [Authorize(Rol   es = "Operator")]
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

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddAssignment([FromBody] Assignment newAssignment)
        {
            try
                {
                    var addedAssignment = await _assignmentService.AddAssignment(newAssignment);
                    return CreatedAtAction(nameof(GetAssignmentById), new { assignmentId = addedAssignment.AssignmentId }, new { message = "Assignment added successfully", assignment = addedAssignment });
                }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // [Authorize]
        [HttpPut("{assignmentId}")]
        public async Task<ActionResult> UpdateAssignment(long assignmentId, [FromBody] Assignment updatedAssignment)
        {
            try
            {
                var success = await _assignmentService.UpdateAssignment(assignmentId, updatedAssignment);

                if (success)
                    return Ok(new { message = "Assignment updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the assignment" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
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
