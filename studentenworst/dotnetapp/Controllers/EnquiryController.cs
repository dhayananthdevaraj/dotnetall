using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    // [Route("api/enquiry")]
    [ApiController]
    // [Authorize(Roles = "Admin, OfficeStaff")] // Adjust authorization based on your requirements
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryService _enquiryService;

        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        //  [HttpPost("api/student/addenquiry")]
        // public IActionResult Swagger3()
        // {
        //     return Ok();
        // }

        [HttpGet("api/enquiry")]
        public async Task<ActionResult<IEnumerable<Enquiry>>> GetAllEnquiries()
        {
            try
            {
                var enquiries = await _enquiryService.GetAllEnquiries();
                return Ok(enquiries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("api/enquiry/{id}")]
        public async Task<ActionResult<Enquiry>> GetEnquiryById(int id)
        {
            try
            {
                var enquiry = await _enquiryService.GetEnquiryById(id);

                if (enquiry == null)
                {
                    return NotFound(new { message = "Cannot find the enquiry" });
                }

                return Ok(enquiry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("api/student/addenquiry")]
        public async Task<ActionResult> AddEnquiry([FromBody] Enquiry newEnquiry)
        {
            try
            {
                var addedEnquiry = await _enquiryService.AddEnquiry(newEnquiry);
                return CreatedAtAction(nameof(GetEnquiryById), new { id = addedEnquiry.EnquiryID }, addedEnquiry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("api/enquiry/{id}")]
        public async Task<ActionResult> UpdateEnquiry(int id, [FromBody] Enquiry updatedEnquiry)
        {
            try
            {
                var success = await _enquiryService.UpdateEnquiry(id, updatedEnquiry);

                if (success)
                    return Ok(new { message = "Enquiry updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the enquiry" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("api/enquiry/{id}")]
        public async Task<ActionResult> DeleteEnquiry(int id)
        {
            try
            {
                var success = await _enquiryService.DeleteEnquiry(id);

                if (success)
                    return Ok(new { message = "Enquiry deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the enquiry" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
