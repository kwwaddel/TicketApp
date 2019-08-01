using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieTix.Contracts.Entities
{
    public class TicketEntity
    {
        public Guid TicketId { get; set; }
        public bool Reserved { get; set; }
        public bool Purchased { get; set; }
        public string UserName { get; set; }
        public SeatEntity Seat { get; set; }
    }
}
