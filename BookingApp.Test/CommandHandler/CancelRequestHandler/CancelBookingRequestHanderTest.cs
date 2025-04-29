using BookingApp.Test.Helper;
using BookingApp.CommandHandlers.CancelBookingCommand;
using   BookingApp.Models;

namespace BookingApp.Test.CommandHandler.CancelRequestHandler
{
    public class CancelBookingRequestHanderTest
    {
        private readonly DbContextHelper _dbHelper = new();

        [Fact]
        public async Task Handle_ValidReference_CancelsBooking()
        {
            // Arrange
            var dbContext = _dbHelper.CreateInMemoryDbContext();
            var member = new Member {Id=1, Name = "Test", Surname = "User", BookingCount = 1 , DateJoined = DateTime.UtcNow};
            var inventory = new Inventory { Id = 1, Description = "Kayak user", Title = "Kayak", RemainingCount = 0 , ExpirationDate = DateTime.UtcNow.AddYears(5)};
            var booking = new Booking
            {
                BookingReference = "INV-2309-TEST123",
                MemberId = 1,
                InventoryItemId = 1,
                IsCancelled = false
            };

            dbContext.Members.Add(member);
            dbContext.Inventory.Add(inventory);
            dbContext.Bookings.Add(booking);
            await dbContext.SaveChangesAsync();

            var handler = new CancelBookingRequestHandler(dbContext);

            // Act
            var result = await handler.Handle(new CancelBookingRequest { BookingRefrence = "INV-2309-TEST123" }, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.True((await dbContext.Bookings.FindAsync(booking.Id))!.IsCancelled);
            Assert.Equal(1, (await dbContext.Inventory.FindAsync(inventory.Id))!.RemainingCount);
            Assert.Equal(0, (await dbContext.Members.FindAsync(member.Id))!.BookingCount);
        }

        [Fact]
        public async Task Handle_InvalidReference_ReturnsFailure()
        {
            // Arrange
            var dbContext = _dbHelper.CreateInMemoryDbContext();
            var handler = new CancelBookingRequestHandler(dbContext);

            // Act
            var result = await handler.Handle(new CancelBookingRequest { BookingRefrence = "INVALID-REF" }, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("not found", result.Message);
        }
    }
}
