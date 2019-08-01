using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Contracts.Models
{
    public class PurchaseTicketInputModel
    {
        public Guid VenueId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; }
    }
}
