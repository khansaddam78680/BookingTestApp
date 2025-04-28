namespace BookingTestApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public DateTime BookedAt { get; set; }
        public string BookingReference { get; set; }

        public Member Member { get; set; }
        public Inventory Inventory { get; set; }
    }
}
