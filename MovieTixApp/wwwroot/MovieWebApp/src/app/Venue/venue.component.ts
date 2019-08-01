import { Component, Inject, OnInit } from '@angular/core';
import { VenueService } from './venue.service';
import { VenueModel, TicketModel, SeatModel } from './venue.model';
import { Response } from '@angular/http';

@Component({
  selector: 'venue',
  templateUrl: './venue.component.html'//,
  //styleUrls: ['./venue.component.css']
})

export class VenueComponent implements OnInit {
  public venues: VenueModel[] = [];
  public selectedVenue: VenueModel;
  public selectedTickets: TicketModel[] = [];
  public purchaseSuccess: boolean;
  public reserved: boolean;
  public userName: string = '';
  public showVenues: boolean = true;
  public reserveTime: number = 60;

  constructor(@Inject(VenueService) private venueService: VenueService) {

  }

  ngOnInit() {
    this.venueService.getVenues().subscribe((response: Response) => {
      console.log(response.json());
      this.venues = response.json();
    });
  }

  selectVenue(venue: VenueModel): void {
    this.showVenues = false;
    this.selectedVenue = venue;
    console.log(venue);
  }

  getAisles(aisle: string): TicketModel[] {
    return this.selectedVenue.tickets.filter((t) => {
      return t.seat.aisle === aisle;
    });
  }

  getRows(row: number): TicketModel[] {
    return this.selectedVenue.tickets.filter((t) => {
      return t.seat.row === row;
    });
  }

  getStyle(ticket: TicketModel): any {
    let style: any = { color: '#e6d819', cursor: 'pointer' };

    if (ticket.purchased || ticket.reserved) {
      style.color = '#484845';
      style.cursor = 'default';
    }
    else if (ticket.selected)
      style.color = '#03fc2c';

    return style;
  }

  selectSeat(ticket: TicketModel): void {
    if (ticket.purchased || ticket.reserved || ticket.selected)
      return;
    
    ticket.selected = true;

    this.selectedTickets.push(ticket);
  }

  clearSelections(): void {
    this.selectedTickets.forEach((t) => {
      t.selected = false;
    });

    this.selectedTickets = [];
    this.purchaseSuccess = false;
    this.reserved = false;
  }

  reserveTickets(): void {
    if (this.userName == null || this.userName == '') {
      window.alert('You must enter a name to reserve tickets');
      return;
    }

    this.venueService.reserveTicket(this.selectedVenue.venueId, this.userName, this.selectedTickets).subscribe((response: Response) => {
      let msg: string = 'Reservation Successful';
      if (parseInt(response.json()) == 1) {
        this.reserved = true;
        this.timer();
      }
      else
        msg = 'Reservation was unsuccessful, the seats are either already reserved or have been purchased';

      window.alert(msg);
    });
  }

  purchaseTickets(): void {
    if (this.userName == null || this.userName == '') {
      window.alert('You must enter a name to purchase tickets');
      return;
    }

    this.venueService.purchaseTicket(this.selectedVenue.venueId, this.userName, this.selectedTickets).subscribe((response: Response) => {
      let msg: string = 'Purchase Successful!';

      if (parseInt(response.json()) == 1) {
        this.purchaseSuccess = true;
        this.reserved = false;
      }
      else
        msg = 'Purchase was unsuccessful, the seats are either reserved or have already been purchased';

      window.alert(msg);
    });
  }

  returnToVenues(): void {
    this.showVenues = true;
    this.selectedVenue = null;
    this.clearSelections();
  }

  timer(): void {
    let self: VenueComponent = this;

    var x = window.setInterval(function (a: VenueComponent) {
      a.reserveTime--;

      if (a.reserveTime == 0) {
        window.clearInterval(x);
        a.reserveTime = 60;
        a.reserved = false;
        window.alert('Your reservation has expired');
      }
    }, 1000, this);
  }
}
