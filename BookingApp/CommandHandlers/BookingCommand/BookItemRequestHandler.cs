using BookingApp.Context;
using BookingApp.Models;
using BookingApp.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.CommandHandlers.BookingCommand
{
    public class BookItemRequestHandler : IRequestHandler<BookItemRequest, BookingResult>
    {
        private readonly BookingDbContext _dbContext;
        private readonly IBookingReferenceService _refService;
        private const int MAX_BOOKINGS = 2;

        public BookItemRequestHandler(BookingDbContext dbContext, IBookingReferenceService refService)
        {
            _dbContext = dbContext;
            _refService = refService;
        }

        public async Task<BookingResult> Handle(BookItemRequest request, CancellationToken cancellationToken)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == request.MemberId);
            
            if (member == null)
            {
                return new BookingResult(false, "Member Not Found");
            }

            if (member.BookingCount >= MAX_BOOKINGS)
            {
                return new BookingResult(false, $"Maximum bookings limit of {MAX_BOOKINGS} reached");
            }

            var inventory = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == request.InventoryId);

            if (member == null)
            {
                return new BookingResult(false, "Inventory Item Not Found");
            }

            if(inventory.RemainingCount <=0)
            {
                return new BookingResult(false, "Item out of stock");
            }

            var bookingReference = _refService.GenerateBookingReference();

            while (await _dbContext.Bookings.AnyAsync(b => b.BookingReference == bookingReference))
            {
                bookingReference = _refService.GenerateBookingReference();
            }

            var booking = new Booking
            {
                MemberId = member.Id,
                InventoryItemId = inventory.Id,
                BookedAt = DateTime.UtcNow,
                BookingReference = bookingReference
            };

            member.BookingCount++;
            inventory.RemainingCount--;

            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();

            return new BookingResult(true, "Booking successful", booking.Id, booking.BookingReference);
        }
    }
}
