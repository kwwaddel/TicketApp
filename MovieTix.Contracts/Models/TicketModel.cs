using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTix.Contracts.Models
{
    public class TicketModel
    {
        public Guid TicketId { get; set; }
        public bool Reserved { get; set; }
        public bool Purchased { get; set; }
        public SeatModel Seat { get; set; }
    }
}
