using MovieTix.Contracts.Interface;
using MovieTix.Contracts.Mappers;
using MovieTix.Contracts.Models;
using System;
using System.Collections.Generic;

namespace MovieTix.Service
{
    public class TicketService: ITicketService
    {
        private ITicketRepository _ticketRepository { get; }
        public TicketService(ITicketRepository repository)
        {
            _ticketRepository = repository;
        }

        public IEnumerable<VenueModel> GetVenues()
        {
            var tickets = _ticketRepository.GetVenues();
            
            return VenueMapper.MapVenues(tickets);
        }

        public bool ReserveTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds)
        {
            return _ticketRepository.ReserveTickets(venueId, userName, ticketIds);
        }

        public bool PurchaseTickets(Guid venueId, string userName, IEnumerable<Guid> ticketIds)
        {
            return _ticketRepository.PurchaseTickets(venueId, userName, ticketIds);
        }
    }
}
