using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRN231.TicketBooking.BusinessObject.Models;

namespace PRN231.TicketBooking.DAO.Data
{
    public class BookingTicketDbContext : IdentityDbContext<Account>, IDbContext
    {
        public BookingTicketDbContext()
        {
        }

        public BookingTicketDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSponsor> EventSponsors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<SeatRank> SeatRanks { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<StaticFile> StaticFiles { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SponsorMoneyHistory> SponsorMoneyHistories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<BusinessObject.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "6a32e12a-60b5-4d93-8306-82231e1232d7",
                Name = "ADMIN",
                ConcurrencyStamp = "6a32e12a-60b5-4d93-8306-82231e1232d7",
                NormalizedName = "admin"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "85b6791c-49d8-4a61-ad0b-8274ec27e764",
                Name = "STAFF",
                ConcurrencyStamp = "85b6791c-49d8-4a61-ad0b-8274ec27e764",
                NormalizedName = "staff"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "814f9270-78f5-4503-b7d3-0c567e5812ba",
                Name = "ORGANIZER",
                ConcurrencyStamp = "814f9270-78f5-4503-b7d3-0c567e5812ba",
                NormalizedName = "organizer"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "02962efa-1273-46c0-b103-7167b1742ef3",
                Name = "CUSTOMER",
                ConcurrencyStamp = "02962efa-1273-46c0-b103-7167b1742ef3",
                NormalizedName = "customer"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "516f3f61-16de-47ee-bde9-c90e4541a272",
                Name = "SPONSOR",
                ConcurrencyStamp = "516f3f61-16de-47ee-bde9-c90e4541a272",
                NormalizedName = "sponsor"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "888f3f00-16de-47ee-bde9-c77e4541a645",
                Name = "PM",
                ConcurrencyStamp = "888f3f00-16de-47ee-bde9-c77e4541a645",
                NormalizedName = "pm"
            });
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", true, true)
                               .Build();
            string cs = config["ConnectionStrings:Host"];
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(cs);
            }
            //optionsBuilder.UseSqlServer(
            //   "server=.;database=TicketBooking;uid=sa;pwd=12345;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }
    }
}