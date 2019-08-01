using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Contracts.Models
{
    public class VenueModel
    {
        public Guid VenueId { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public int MaxRow { get; set; }
        public string MaxAisle { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; }
        //public List<List<TicketModel>> SeatingArrangement { get; set; }
    }
}
