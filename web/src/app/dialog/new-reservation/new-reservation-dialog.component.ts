import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material';
import { ReservationItem } from '../../entity/ReservationItem';
import * as moment from 'moment';
import { InfoService } from '../../service/info.service';
import { ReservationService } from '../../service/reservation.service';
import { DateTimeInterval } from '../../entity/DateTimeInterval';

@Component({
  selector: 'app-new-reservation',
  templateUrl: './new-reservation-dialog.component.html',
  styleUrls: ['./new-reservation-dialog.component.scss']
})
export class NewReservationDialogComponent implements OnInit {

  public formFromDate = new FormControl(new Date());
  public formFromTime = new FormControl('00:00');
  public formToDate = new FormControl(new Date());
  public formToTime = new FormControl('00:00');
  public formServerType = new FormControl(null);
  public formDeployType = new FormControl(null);

  public timeSpan = null;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ReservationItem,
    public infoService: InfoService,
    public reservationService: ReservationService,
  ) {
  }

  ngOnInit() {
  }

  private getDatesStr(): [string, string] {
    const fromDateStr = this.formFromDate.value.toJSON().split('T')[0];
    const fromStr = `${fromDateStr} ${this.formFromTime.value}`;
    const toDateStr = this.formToDate.value.toJSON().split('T')[0];
    const toStr = `${toDateStr} ${this.formToTime.value}`;

    return [fromStr, toStr];
  }

  onDateTimeChange() {
    const [fromStr, toStr] = this.getDatesStr();
    const format = 'YYYY-MM-DD HH:mm';

    const ms = moment(toStr, format).diff(moment(fromStr, format));
    const d = moment.duration(ms);

    const dateParts = [];
    if (d.days() > 0) {
      dateParts.push(d.days() + ' days');
    }
    if (d.hours() > 0) {
      dateParts.push(d.hours() + ' hours');
    }
    if (d.minutes() > 0) {
      dateParts.push(d.minutes() + ' mins');
    }

    this.timeSpan = dateParts.join(', ');
  }

  async onSubmit() {
    const [fromStr, toStr] = this.getDatesStr();
    const from = new Date(fromStr);
    const to = new Date(toStr);
    await this.reservationService.createReservationAsync(
      new ReservationItem(
        +this.formDeployType.value,
        +this.formServerType.value,
        'fix/test/branch',
        'admin',
        new DateTimeInterval(from.toJSON(), to.toJSON()),
        null
      ));
  }

}
