using Microsoft.EntityFrameworkCore;
using MovieTix.Contracts.Entities;
using MovieTix.Contracts.Interface;
using MovieTix.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MovieTix.Domain
{
    public class TicketRepository: ITicketRepository
    {
        private MovieTixContext _context { get; }
        private readonly object _ticketLock = new object();
        public TicketRepository(MovieTixContext context)
        {
            _context = context;
        }

        public IEnumerable<VenueEntity> GetVenues()
        {
            return _context.Venues.ToList();
        }

        public bool ReserveTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            lock (_ticketLock)
            {
                var t = _context.Venues.Where(x => x.VenueId == venueId).FirstOrDefault();
                var selected = t.Tickets.Where(y => ticketIds.Contains(y.TicketId));
                bool success = true;

                selected.ToList().ForEach(x => {
                    if (!x.Reserved && !x.Purchased)
                    {
                        x.Reserved = true;
                        x.UserName = userName;
                    }
                    else
                        success = false;
                });

                if (success)
                {
                    _context.SaveChanges();
                    new Thread((ParameterizedThreadStart)delegate { ReserveTime(60, selected); }).Start();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool PurchaseTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            
            lock (_ticketLock)
            {
                var t = _context.Venues.Where(x => x.VenueId == venueId).FirstOrDefault();
                var selected = t.Tickets.Where(y => ticketIds.Contains(y.TicketId));
                bool success = true;

                selected.ToList().ForEach(x => {
                    if (!x.Purchased && (!x.Reserved || (!string.IsNullOrWhiteSpace(x.UserName) && x.UserName == userName)))
                    {
                        x.Purchased = true;
                    }
                    else
                        success = false;
                });

                if (success)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        private void ReserveTime(int seconds, IEnumerable<TicketEntity> tickets)
        {
            Thread.Sleep(seconds * 1000);

            tickets.ToList().ForEach(x =>
            {
                x.Reserved = false;
                _context.Entry(_context.Tickets.Find(x.TicketId)).CurrentValues.SetValues(x);
            });

            _context.SaveChanges();
        }
    }
}
