using BookingApp.Context;
using BookingApp.CsvModels;
using BookingApp.Models;
using CsvHelper;
using System.Globalization;

namespace BookingApp.Services
{
    public class CsvDataUploadService : ICsvDataUploadService
    {
        private readonly BookingDbContext _dbContext;

        public CsvDataUploadService(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UploadMembersFromCsv(IFormFile file)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var data = csv.GetRecords<MembersCsv>().ToList();

                var membersToAdd = data.Select(x => new Member
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    BookingCount = x.BookingCount,
                    DateJoined = x.DateJoined,
                }).ToList();

                await _dbContext.Members.AddRangeAsync(membersToAdd);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading members : {0}", ex.ToString());
                return false;
            }
        }

        public async Task<bool> UploadInventoryFromCsv(IFormFile file)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var data = csv.GetRecords<InventoryCsv>().ToList();

                var inventoryToAdd = data.Select(x => new Inventory
                {
                    Title = x.Title,
                    Description = x.Description,
                    RemainingCount = x.RemainingCount,
                    ExpirationDate = DateTime.Parse(x.ExpirationDate)
                }).ToList();

                await _dbContext.Inventory.AddRangeAsync(inventoryToAdd);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading inventory : {0}", ex.ToString());
                return false;
            }
        }
    }
}
