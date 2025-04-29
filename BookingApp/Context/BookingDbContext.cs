using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Context
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) :base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Member>(entity =>
            {
                entity.HasIndex(x => new { x.Name, x.Surname }).IsUnique();
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Surname).IsRequired();
                entity.Property(x => x.DateJoined).IsRequired();
            });
            builder.Entity<Inventory>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.Title).IsRequired();
                entity.Property(x => x.Description).IsRequired();
                entity.Property(x => x.ExpirationDate).IsRequired();
            });
            builder.Entity<Booking>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasIndex(x => x.BookingReference).IsUnique();
            });
        }
    }
}
