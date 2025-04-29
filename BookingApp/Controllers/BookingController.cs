using BookingApp.CommandHandlers.BookingCommand;
using BookingApp.CommandHandlers.CancelBookingCommand;
using BookingApp.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookItem([FromBody]BookingRequest request)
        {
            var result = await _mediator.Send(new BookItemRequest { MemberId = request.MemberId, InventoryId = request.InventoryId });
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("cancel/{bookingReference}")]
        public async Task<IActionResult> CancelBooking([FromRoute]string bookingReference)
        {
            var result = await _mediator.Send(new CancelBookingRequest { BookingRefrence = bookingReference });
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
