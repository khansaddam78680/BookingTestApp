using MediatR;

namespace BookingApp.CommandHandlers.BookingCommand
{
    public class BookItemRequest : IRequest<BookingResult>
    {
        public int MemberId { get; set; }
        public int InventoryId { get; set; }
    }
}
