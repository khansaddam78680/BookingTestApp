using BookingApp.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly BookingDbContext _dbContext;
        public DataController(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAllMembers")]
        public async Task<IActionResult> GetAllMembersList()
        {
            return Ok(await _dbContext.Members.ToListAsync());
        }

        [HttpGet("getAllInventory")]
        public async Task<IActionResult> GetAllInventoryList()
        {
            return Ok(await _dbContext.Inventory.ToListAsync());
        }

        [HttpGet("getAllBookings")]
        public async Task<IActionResult> GetAllBookingsList()
        {
            return Ok(await _dbContext.Bookings.ToListAsync());
        }
    }
}
