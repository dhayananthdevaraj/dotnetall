using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/fruits")]
    [ApiController]
    [Authorize]
    public class FruitController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruits = await Task.FromResult(new string[] { "apple", "bananana", "kiwi" });
            return Ok(fruits);
        }
    }
}
