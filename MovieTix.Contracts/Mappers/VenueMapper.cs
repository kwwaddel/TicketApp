using MovieTix.Contracts.Entities;
using MovieTix.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTix.Contracts.Mappers
{
    public static class VenueMapper
    {
        public static IEnumerable<VenueModel> MapVenues(IEnumerable<VenueEntity> e)
        {
            var result = new List<VenueModel>();
            e.ToList().ForEach(x => result.Add(MapVenue(x)));

            return result;
        }
        public static VenueModel MapVenue(VenueEntity e)
        {
            var m = new VenueModel();

            m.VenueId = e.VenueId;
            m.Address = e.Address;
            m.EventName = e.EventName;
            m.MaxAisle = e.MaxAisle;
            m.MaxRow = e.MaxRow;
            m.Tickets = e.Tickets.Select(x => MapTicket(x));
            //m.SeatingArrangement = new List<KeyValuePair<SeatModel, TicketModel>>();
            //m.Tickets.ToList().ForEach(x => m.SeatingArrangement.ToList().Add(new KeyValuePair<SeatModel, TicketModel>(x.Seat, x)));

            return m;
        }

        public static TicketModel MapTicket(TicketEntity e)
        {
            return new TicketModel { TicketId = e.TicketId, Purchased = e.Purchased, Reserved = e.Reserved, Seat = MapSeat(e.Seat) };
        }

        public static SeatModel MapSeat(SeatEntity e)
        {
            return new SeatModel { Aisle = e.Aisle, Row = e.Row };
        }
    }
}
