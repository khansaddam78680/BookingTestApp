using MediatR;

namespace BookingApp.CommandHandlers.CancelBookingCommand
{
    public class CancelBookingRequest : IRequest<CancelBookingResult>
    {
        public string BookingRefrence { get; set; }
    }
}
