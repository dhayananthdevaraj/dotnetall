// IssueService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class IssueService
    {
        private readonly ApplicationDbContext _context;

        public IssueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Issue>> GetAllIssues()
        {
            return await _context.Issues
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Assignment)
                    .ThenInclude(a => a.Container)
                .ToListAsync();
        }

        public async Task<Issue> ReportIssue(Issue newIssue)
        {
            _context.Issues.Add(newIssue);
            await _context.SaveChangesAsync();
            return newIssue;
        }
    }
}
