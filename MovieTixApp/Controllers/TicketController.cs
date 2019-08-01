using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieTix.Contracts.Interface;
using MovieTix.Contracts.Models;

namespace MovieTixApp
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private ITicketService _ticketService { get; }
        public TicketController(ITicketService service)
        {
            _ticketService = service;
        }

        [Route("GetVenues")]
        [HttpGet]
        public IEnumerable<VenueModel> GetVenues()
        {
            return _ticketService.GetVenues();
        }

        [Route("ReserveTickets")]
        [HttpPost]
        public int ReserveTickets([FromBody]PurchaseTicketInputModel input)
        {
            var b = _ticketService.ReserveTickets(input.VenueId, input.UserName, input.Tickets.Select(x => x.TicketId));
            if (b)
                return 1;
            else
                return -1;
        }

        [Route("PurchaseTickets")]
        [HttpPost]
        public int PurchaseTickets([FromBody]PurchaseTicketInputModel input)
        {
            var b = _ticketService.PurchaseTickets(input.VenueId, input.UserName, input.Tickets.Select(x => x.TicketId));

            if (b)
                return 1;
            else
                return -1;
        }
    }
}
