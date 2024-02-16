// IssueController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;

namespace dotnetapp.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        // POST: api/Issue
        [HttpPost]
        public async Task<ActionResult> ReportIssue([FromBody] Issue newIssue)
        {
            try
            {
                var addedIssue = await _issueService.ReportIssue(newIssue);
                return CreatedAtAction(nameof(ReportIssue), new { id = addedIssue.IssueId }, addedIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Issues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> ViewAllReportedIssues()
        {
            try
            {
                var issues = await _issueService.ViewAllReportedIssues();
                return Ok(issues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
