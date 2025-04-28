namespace BookingTestApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookingCount { get; set; }
        public DateTime DateJoined { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
