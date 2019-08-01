using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieTix.Contracts.Entities
{
    public class VenueEntity
    {
        public Guid VenueId { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public int MaxRow { get; set; }
        public string MaxAisle { get; set; }
        public IEnumerable<TicketEntity> Tickets { get; set; }
    }
}
