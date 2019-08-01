using Microsoft.EntityFrameworkCore;
using MovieTix.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Domain
{
    public class MovieTixContext: DbContext
    {
        public virtual DbSet<VenueEntity> Venues { get; set; }
        public virtual DbSet<TicketEntity> Tickets { get; set; }

        public MovieTixContext()
        {
            Database.EnsureDeleted();

            var testEntity1 = new VenueEntity {
                Address = "Madison Square Garden",
                EventName = "Bruins @ Rangers",
                MaxAisle = "I",
                MaxRow = 7,
                VenueId = Guid.NewGuid(),
                Tickets = GenerateTickets(7, 'I')
            };

            var testEntity2 = new VenueEntity
            {
                Address = "Regal Cinema",
                EventName = "Avengers: Endgame",
                MaxAisle = "F",
                MaxRow = 6,
                VenueId = Guid.NewGuid(),
                Tickets = GenerateTickets(6, 'F')
            };

            var testEntity3 = new VenueEntity
            {
                Address = "Alamo Drafthouse",
                EventName = "Monty Python and the Holy Grail",
                MaxAisle = "D",
                MaxRow = 8,
                VenueId = Guid.NewGuid(),
                Tickets = GenerateTickets(8, 'D')
            };

            Venues.Add(testEntity1);
            Venues.Add(testEntity2);
            Venues.Add(testEntity3);

            SaveChanges();
        }

        private List<TicketEntity> GenerateTickets(int maxRow, char maxAisle)
        {
            Random rand = new Random();
            var tickets = new List<TicketEntity>();

            for (int i = 1; i <= maxRow; i++)
            {
                for (char c = 'A'; c <= maxAisle; c++)
                {
                    tickets.Add(new TicketEntity
                    {
                        TicketId = Guid.NewGuid(),
                        Purchased = rand.Next(0, 5) < 3? true: false,
                        Reserved = false,
                        Seat = new SeatEntity
                        {
                            SeatId = Guid.NewGuid(),
                            Aisle = c.ToString(),
                            Row = i
                        }
                    });
                }
            }

            return tickets;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("VenueDB");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VenueEntity>().HasKey(e => e.VenueId);
            modelBuilder.Entity<VenueEntity>()
                .HasMany(e => e.Tickets)
                .WithOne();

            modelBuilder.Entity<TicketEntity>().HasKey(e => e.TicketId);
            modelBuilder.Entity<TicketEntity>().HasOne(e => e.Seat)
                .WithOne()
                .HasForeignKey<SeatEntity>(e => e.SeatId);

            modelBuilder.Entity<SeatEntity>().HasKey(e => e.SeatId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
