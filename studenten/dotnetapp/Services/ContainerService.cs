// ContainerService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class ContainerService
    {
        private readonly ApplicationDbContext _context;

        public ContainerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Container>> GetAllContainers()
        {
            return await _context.Containers.ToListAsync();
        }

        public async Task<Container> AddContainer(Container container)
        {
            _context.Containers.Add(container);
            await _context.SaveChangesAsync();
            return container;
        }

        public async Task<bool> UpdateContainer(long containerId, Container container)
        {
            var existingContainer = await _context.Containers.FirstOrDefaultAsync(c => c.ContainerId == containerId);

            if (existingContainer == null)
                return false;

            existingContainer.Type = container.Type;
            existingContainer.Status = container.Status;
            existingContainer.Capacity = container.Capacity;
            existingContainer.Weight = container.Weight;
            existingContainer.Owner = container.Owner;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteContainer(long containerId)
        {
            var container = await _context.Containers.FirstOrDefaultAsync(c => c.ContainerId == containerId);

            if (container == null)
                return false;

            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();
            return true;
        }

           public async Task<Container> GetContainerById(long containerId)
{
    return await Task.FromResult(_context.Containers.FirstOrDefault(c => c.ContainerId == containerId));
}
    }
}
