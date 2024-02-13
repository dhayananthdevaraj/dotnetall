using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models; // Assuming your models are in the DotNetApp.Models namespace

[Route("api/job")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Replace YourDbContext with your actual DbContext class

    public JobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs([FromQuery] int? sortValue = 1, [FromQuery] string? searchValue = "")
    {

        var jobs = await _context.Jobs
        .ToListAsync(); // Retrieve all jobs from the database

    if (!string.IsNullOrEmpty(searchValue))
    {

        Console.WriteLine("!string.IsNullOrEmpty(searchValue)",!string.IsNullOrEmpty(searchValue));
        var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        jobs = jobs
            .Where(job => searchRegex.IsMatch(job.Title)).ToList(); // Apply search filter
    }
    // var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    //         jobs = jobs
    //             .Where(job => searchRegex.IsMatch(job.Title)).ToList(); // Convert back to List

                                                      // .OrderBy(job => job.StartDate) // Sort based on StartDate and sortValue
        if (sortValue == -1)
        {
            jobs = jobs.OrderByDescending(job => job.StartDate).ToList(); // Sort in descending order
        }
        else
        {
            jobs = jobs.OrderBy(job => job.StartDate).ToList(); // Sort in ascending order (default)
        }

        return Ok(jobs);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<Job>> GetJobById(int jobId)
    {
        var job = await _context.Jobs
            .FirstOrDefaultAsync(job => job.JobId == jobId);

        if (job == null)
        {
            return NotFound(new { message = "Cannot find any job" });
        }

        return Ok(job);
    }

    [HttpPost]
    public async Task<ActionResult> AddJob([FromBody] Job job)
    {
        try
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("{jobId}")]
    public async Task<ActionResult> UpdateJob(int jobId, [FromBody] Job job)
    {
        try
        {
            var existingJob = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);

            if (existingJob == null)
            {
                return NotFound(new { message = "Cannot find any job" });
            }
            job.JobId = jobId;


            _context.Entry(existingJob).CurrentValues.SetValues(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{jobId}")]
    public async Task<ActionResult> DeleteJob(int jobId)
    {
        try
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);

            if (job == null)
            {
                return NotFound(new { message = "Cannot find any job" });
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobsByUserId(int userId, [FromQuery] string? searchValue = null)
    {

        // var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Console.WriteLine("searchValue" + searchValue);
        var jobs = await _context.Jobs
            // .Where(job => job.UserId == userId && searchRegex.IsMatch(job.Title))
            .ToListAsync();

        jobs = jobs
            .Where(job => job.UserId == userId)
                // .OrderBy(job => job.StartDate) // Sort based on StartDate and sortValue
                .ToList();

        if (!string.IsNullOrEmpty(searchValue))
        {
            var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            jobs = jobs.Where(job => searchRegex.IsMatch(job.Title)).ToList();
        }


        return Ok(jobs);
    }
}
