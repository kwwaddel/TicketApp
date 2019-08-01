using MovieTix.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Contracts.Interface
{
    public interface ITicketRepository
    {
        IEnumerable<VenueEntity> GetVenues();
        bool ReserveTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds);
        bool PurchaseTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds);
    }
}
