using MovieTix.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Contracts.Interface
{
    public interface ITicketService
    {
        IEnumerable<VenueModel> GetVenues();
        bool ReserveTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds);
        bool PurchaseTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds);
    }
}
