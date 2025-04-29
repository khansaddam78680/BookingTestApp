using CsvHelper.Configuration.Attributes;

namespace BookingApp.CsvModels
{
    public class InventoryCsv
    {
        [Name("title")]
        public string Title { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Name("remaining_count")]
        public int RemainingCount { get; set; }

        [Name("expiration_date")]
        public string ExpirationDate { get; set; }
    }
}
