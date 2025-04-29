using BookingApp.Context;
using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Test.Helper
{
    public class DbContextHelper
    {
        public BookingDbContext CreateInMemoryDbContext(string databaseName = null)
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase("BookingAppDB")
                .Options;

            return new BookingDbContext(options);
        }
    }
}
