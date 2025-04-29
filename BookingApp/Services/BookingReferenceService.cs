namespace BookingApp.Services
{
    public class BookingReferenceService : IBookingReferenceService
    {
        private const string PREFIX = "INV";
        private const int REFERENCE_LENGTH = 8;

        public string GenerateBookingReference()
        {
            var random = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // Excluded confusing characters
            var randomPart = new string(Enumerable.Repeat(chars, REFERENCE_LENGTH)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{PREFIX}-{DateTime.UtcNow:yyMM}-{randomPart}";
        }
    }
}
