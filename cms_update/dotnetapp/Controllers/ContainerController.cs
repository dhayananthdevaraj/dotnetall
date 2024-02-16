// ContainerController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/container")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Container>>> GetAllContainers()
        {
            try
            {
                var containers = await _containerService.GetAllContainers();
                return Ok(containers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddContainer([FromBody] Container container)
        {
            try
            {
                var addedContainer = await _containerService.AddContainer(container);
                return StatusCode(201, addedContainer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{containerId}")]
        public async Task<ActionResult> UpdateContainer(long containerId, [FromBody] Container container)
        {
            try
            {
                var success = await _containerService.UpdateContainer(containerId, container);
                if (success)
                    return Ok(new { message = "Container updated successfully" });
                else
                    return NotFound(new { message = "Cannot find the container" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{containerId}")]
        public async Task<ActionResult> DeleteContainer(long containerId)
        {
            try
            {
                var success = await _containerService.DeleteContainer(containerId);
                if (success)
                    return Ok(new { message = "Container deleted successfully" });
                else
                    return NotFound(new { message = "Cannot find the container" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
