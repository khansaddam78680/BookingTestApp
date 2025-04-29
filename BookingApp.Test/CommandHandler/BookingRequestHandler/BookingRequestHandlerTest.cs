using BookingApp.Test.Helper;
using BookingApp.CommandHandlers.BookingCommand;
using BookingApp.Models;
using BookingApp.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookingApp.Test.CommandHandler.BookingRequestHandler
{
    public class BookingRequestHandlerTest
    {
        private readonly DbContextHelper _dbHelper = new();
        private readonly Mock<IBookingReferenceService> _refServiceMock = new();

        [Fact]
        public async Task Handle_ValidRequest_CreatesBookingWithReference()
        {
            // Arrange
            var dbContext = _dbHelper.CreateInMemoryDbContext();
            var member = new Member { Id = 1, Name = "Test", Surname = "User", BookingCount = 0, DateJoined = DateTime.UtcNow };
            var inventory = new Inventory { Id = 1, Description = "Kayak user", Title = "Kayak", RemainingCount = 5, ExpirationDate = DateTime.UtcNow.AddYears(5) };

            dbContext.AddRange(member, inventory);
            await dbContext.SaveChangesAsync();

            _refServiceMock.Setup(x => x.GenerateBookingReference())
            .Returns("INV-2309-ABC123");

            var handler = new BookItemRequestHandler(dbContext, _refServiceMock.Object);

            // Act
            var result = await handler.Handle(new BookItemRequest { MemberId = member.Id, InventoryId = inventory.Id }, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("INV-2309-ABC123", result.BookingRefrence);
            Assert.Equal(1, await dbContext.Bookings.CountAsync());
            Assert.Equal(1, (await dbContext.Members.FindAsync(member.Id))!.BookingCount);
            Assert.Equal(4, (await dbContext.Inventory.FindAsync(inventory.Id))!.RemainingCount);
        }

        [Fact]
        public async Task Handle_MaxBookingsReached_ReturnsFailure()
        {
            // Arrange
            var dbContext = _dbHelper.CreateInMemoryDbContext();
            var member = new Member { Id = 1, Name = "Test", Surname = "User", BookingCount = 2, DateJoined = DateTime.UtcNow };
            var inventory = new Inventory { Id = 1, Description = "Kayak user", Title = "Kayak", RemainingCount = 5, ExpirationDate = DateTime.UtcNow.AddYears(5) };

            dbContext.AddRange(member, inventory);
            await dbContext.SaveChangesAsync();

            var handler = new BookItemRequestHandler(dbContext, _refServiceMock.Object);

            // Act
            var result = await handler.Handle(new BookItemRequest { MemberId = member.Id, InventoryId = inventory.Id }, CancellationToken.None);

            //Assert
            Assert.False(result.Success);
            Assert.Contains("Maximum bookings limit", result.Message);
        }
    }
}