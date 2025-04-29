namespace BookingApp.CommandHandlers.BookingCommand
{
    public record BookingResult(bool Success, string Message, int? BookingId = null, string BookingRefrence = null);
}
