import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { VenueModel, TicketModel, SeatModel } from './venue.model';
import { Observable } from 'rxjs/Observable';

export class VenueService implements Injectable {
  constructor(@Inject(Http) private http: Http) {
  }

  public getVenues(): Observable<any> {
    return this.http.get('http://localhost:34809/api/Ticket/GetVenues');
  }

  public reserveTicket(venueId: string, userName: string, tickets: TicketModel[]): Observable<any> {
    return this.http.post('http://localhost:34809/api/Ticket/ReserveTickets', { 'venueId': venueId, 'userName': userName, 'tickets': tickets });
  }

  public purchaseTicket(venueId: string, userName: string, tickets: TicketModel[]): Observable<any> {
    return this.http.post('http://localhost:34809/api/Ticket/PurchaseTickets', { 'venueId': venueId, 'userName': userName, 'tickets': tickets });
  }
}
