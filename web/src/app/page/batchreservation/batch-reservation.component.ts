import { Component, OnInit } from '@angular/core';
import { InfoService } from '../../service/info.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-batchreservation',
  templateUrl: './batch-reservation.component.html',
  styleUrls: ['./batch-reservation.component.scss']
})
export class BatchReservationComponent implements OnInit {

  public formServerType = new FormControl('');
  public formDeployType = new FormControl('');
  public reservationPeriod: string = '';

  constructor(
    public infoService: InfoService
  ) { }

  ngOnInit() {
  }

  calculateClosestPeriod() {
    const now = +new Date();
    this.reservationPeriod = `${new Date(now).toJSON()} --> ${new Date(now + 1e6).toJSON()}`;
  }

}
