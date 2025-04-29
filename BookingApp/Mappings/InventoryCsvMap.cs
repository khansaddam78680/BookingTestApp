using BookingApp.CsvModels;
using CsvHelper.Configuration;
using System.Globalization;

namespace BookingApp.Mappings
{
    public sealed class InventoryCsvMap : ClassMap<InventoryCsv>
    {
        public InventoryCsvMap() 
        {
            Map(x => x.ExpirationDate).Name("expiration_date").TypeConverterOption.Format("dd-MM-yy")
                .TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeLocal);
        }
    }
}
