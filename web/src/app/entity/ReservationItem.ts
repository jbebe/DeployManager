import { DateTimeInterval } from './DateTimeInterval';

export class ReservationItem {

  constructor(
    public deployType: number,
    public serverType: number,
    public branchName: string,
    public author: string,
    public reservedInterval: DateTimeInterval,
    public previousReservation: ReservationItem = null,
  ) {}
}
