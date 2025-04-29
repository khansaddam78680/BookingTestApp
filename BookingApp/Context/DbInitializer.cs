using BookingApp.Context;
using BookingApp.CsvModels;
using BookingApp.Mappings;
using BookingApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BookingApp.Infrastructure
{
    public class DbInitializer
    {
        private readonly BookingDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public DbInitializer(BookingDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();

            if (_dbContext.Members.Any() || _dbContext.Inventory.Any())
                return;

            await SeedMembersDataAsync();
            await SeedInventoryData();
        }

        private async Task SeedMembersDataAsync()
        {
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "AutoSeedData", "members.csv");
                if(!File.Exists(path))
                {
                    Console.WriteLine("Members CSV file not found at {Path}", path);
                    return;
                }

                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var data = csv.GetRecords<MembersCsv>().ToList();

                var membersToAdd = data.Select(x => new Member
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    BookingCount = x.BookingCount,
                    DateJoined = x.DateJoined
                }).ToList();

                await _dbContext.Members.AddRangeAsync(membersToAdd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error seeding members : {0}", ex.ToString());
            }
        }
        private async Task SeedInventoryData()
        {
            try
            {
                var path = Path.Combine(_env.ContentRootPath, "AutoSeedData", "inventory.csv");
                if (!File.Exists(path))
                {
                    Console.WriteLine("Inventory CSV file not found at {Path}", path);
                    return;
                }

                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                //csv.Context.RegisterClassMap<InventoryCsvMap>();
                var data = csv.GetRecords<InventoryCsv>().ToList();

                var inventoryToAdd = data.Select(x => new Inventory
                {
                    Title = x.Title,
                    Description = x.Description,
                    RemainingCount = x.RemainingCount,
                    ExpirationDate = DateTime.Parse(x.ExpirationDate),
                }).ToList();

                await _dbContext.Inventory.AddRangeAsync(inventoryToAdd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error seeding inventory : {0}", ex.ToString());
            }
        }
    }
}
