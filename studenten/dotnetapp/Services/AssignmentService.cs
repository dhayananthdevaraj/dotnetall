// AssignmentService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class AssignmentService
    {
        private readonly ApplicationDbContext _context;

        public AssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            return await _context.Assignments
                .Include(a => a.Container)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(long assignmentId)
        {
            return await _context.Assignments
                .Include(a => a.Container)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByUserId(long userId)
        {
            return await _context.Assignments
                .Where(a => a.UserId == userId)
                .Include(a => a.Container)
                .ToListAsync();
        }

        public async Task<Assignment> AddAssignment(Assignment newAssignment)
        {
            _context.Assignments.Add(newAssignment);
            await _context.SaveChangesAsync();
            return newAssignment;
        }

        public async Task<bool> UpdateAssignment(long assignmentId, Assignment updatedAssignment)
        {
            var existingAssignment = await _context.Assignments.FindAsync(assignmentId);

            if (existingAssignment == null)
                return false;

            existingAssignment.Status = updatedAssignment.Status;
            existingAssignment.UpdateTime = updatedAssignment.UpdateTime;
            existingAssignment.Route = updatedAssignment.Route;
            existingAssignment.Shipment = updatedAssignment.Shipment;
            existingAssignment.Destination = updatedAssignment.Destination;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssignment(long assignmentId)
        {
            var assignment = await _context.Assignments.FindAsync(assignmentId);

            if (assignment == null)
                return false;

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
