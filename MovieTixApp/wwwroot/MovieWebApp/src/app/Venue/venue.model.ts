export class VenueModel {
  venueId: string;
  eventName: string;
  address: string;
  maxRow: number;
  maxAisle: string;
  tickets: TicketModel[];
}

export class TicketModel {
  reserved: boolean;
  purchased: boolean;
  selected: boolean;
  seat: SeatModel;
}

export class SeatModel {
  aisle: string;
  row: number;
}
