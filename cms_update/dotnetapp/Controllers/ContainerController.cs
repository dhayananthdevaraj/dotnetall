// ContainerController.cs
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
    [Route("api/container")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly ContainerService _containerService;

        public ContainerController(ContainerService containerService)
        {
            _containerService = containerService;
        }

       [Authorize]
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
 
 
            [Authorize]                
            [HttpPost]
            public async Task<ActionResult> AddContainer([FromBody] Container container)
            {
                try
                {
                      var addedContainer = await _containerService.AddContainer(container);
                      return StatusCode(201, new { message = "Container added successfully", container = addedContainer });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }
            }

        


        [Authorize]
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

       [Authorize]
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
