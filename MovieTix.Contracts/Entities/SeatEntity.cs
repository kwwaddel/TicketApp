using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTix.Contracts.Entities
{
    public class SeatEntity
    {
        public Guid SeatId { get; set; }
        public int Row { get; set; }
        public string Aisle { get; set; }
    }
}
