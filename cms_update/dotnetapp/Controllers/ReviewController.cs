using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "InventoryManager")]
        [HttpPost]
        public IActionResult AddReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Invalid review data");
            }

            _reviewService.AddReview(review);

            return Ok("Review added successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _reviewService.GetAllReviews();

            return Ok(reviews);
        }
    }
}
