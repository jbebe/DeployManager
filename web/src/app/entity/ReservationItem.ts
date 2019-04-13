export class ReservationItem {

  constructor(
    public Id: string,
    public DeployType: number,
    public ServerType: number,
    public BranchName: string,
    public Author: string,
    public Start: Date,
    public End: Date,
    public Previous: string,
  ) {}
}
