using BookingApp.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.CommandHandlers.CancelBookingCommand
{
    public class CancelBookingRequestHandler : IRequestHandler<CancelBookingRequest, CancelBookingResult>
    {
        private readonly BookingDbContext _dbContext;

        public CancelBookingRequestHandler(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CancelBookingResult> Handle(CancelBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = await _dbContext.Bookings
                .FirstOrDefaultAsync(x => x.BookingReference == request.BookingRefrence);
            if (booking == null)
            {
                return new CancelBookingResult(false, "Booking not found");
            }

            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == booking.MemberId);
            var inventory = await _dbContext.Inventory.FirstOrDefaultAsync(x => x.Id == booking.InventoryItemId);
            if (booking.IsCancelled)
            {
                return new CancelBookingResult(false, "Booking already cancelled");
            }

            booking.IsCancelled = true;
            member.BookingCount = Math.Max(0, member.BookingCount - 1);
            inventory.RemainingCount++;

            await _dbContext.SaveChangesAsync();
            return new CancelBookingResult(true, "Booking cancelled successfully");
        }
    }
}
