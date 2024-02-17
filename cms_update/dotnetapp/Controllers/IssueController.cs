// IssueController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueService _issueService;

        public IssueController(IssueService issueService)
        {
            _issueService = issueService;
        }

        // POST: api/Issue
        [Authorize(Roles = "Operator")]
        [HttpPost]
        public async Task<ActionResult> ReportIssue([FromBody] Issue newIssue)
        {
           try
                {
                    var reportedIssue = await _issueService.ReportIssue(newIssue);
                    return CreatedAtAction(nameof(ReportIssue), new { id = reportedIssue.IssueId }, new { message = "Issue reported successfully", issue = reportedIssue });
                }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Issues
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> ViewAllReportedIssues()
        {
            try
            {
                var issues = await _issueService.GetAllIssues();
                return Ok(issues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
