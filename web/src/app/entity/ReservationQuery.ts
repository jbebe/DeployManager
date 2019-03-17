

export class ReservationQuery {

  constructor(
    public deployType: number | null,
    public serverType: number | null,
    public from: string,
    public to: string,
  ) { }
}
