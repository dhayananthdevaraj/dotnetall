//using dotnetapp.Data;
//using dotnetapp.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace dotnetapp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookingController : ControllerBase
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public BookingController(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // GET: api/Booking
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
//        {
//            return await _dbContext.Bookings.ToListAsync();
//        }

//        // GET: api/Booking/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Booking>> GetBooking(int id)
//        {
//            var booking = await _dbContext.Bookings.FindAsync(id);

//            if (booking == null)
//            {
//                return NotFound();
//            }

//            return booking;
//        }

//        // POST: api/Booking
//        [HttpPost]
//        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
//        {

//            _dbContext.Bookings.Add(booking);
//            await _dbContext.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, booking);
//        }

//        // PUT: api/Booking/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBooking(int id, Booking booking)
//        {
//            if (id != booking.BookingID)
//            {
//                return BadRequest();
//            }

//            _dbContext.Entry(booking).State = EntityState.Modified;

//            try
//            {
//                await _dbContext.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookingExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // DELETE: api/Booking/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBooking(int id)
//        {
//            var booking = await _dbContext.Bookings.FindAsync(id);
//            if (booking == null)
//            {
//                return NotFound();
//            }

//            _dbContext.Bookings.Remove(booking);
//            await _dbContext.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool BookingExists(int id)
//        {
//            return _dbContext.Bookings.Any(e => e.BookingID == id);
//        }
//    }
//}
