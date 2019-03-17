import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Network, DataSet, Node, Edge, IdType, Timeline } from 'vis';
import { MatDialog } from '@angular/material';
import { NewReservationDialogComponent } from '../../dialog/new-reservation/new-reservation-dialog.component';
import { ReservationItem } from '../../entity/ReservationItem';
import { InfoService } from '../../service/info.service';
import { DateTimeInterval } from '../../entity/DateTimeInterval';
import { ReservationService } from '../../service/reservation.service';
import { ReservationQuery } from '../../entity/ReservationQuery';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {

  constructor(
    public dialog: MatDialog,
    public infoService: InfoService,
    public reservationService: ReservationService,
  ) {

  }

  async ngOnInit() {
    await this.drawAsync();
  }

  private async drawAsync() {
    const deployTypes = await this.infoService.getDeployTypesAsync();
    const serverTypes = await this.infoService.getServerTypesAsync();

    const serverInfos = await this.infoService.getServerInstancesAsync();
    const now = moment().minutes(0).seconds(0).milliseconds(0);
    const itemCount = 60;

    const groups = new DataSet();

    // Create main groups (deploy types)
    groups.add(deployTypes.map((dt) => ({
      id: `deploy_${dt.id}`,
      content: dt.name,
      nestedGroups: serverTypes.map((st) => this.getServerId(st.id, dt.id)),
      showNested: false,
    })));

    // Create sub groups (server types)
    groups.add(serverTypes.reduce((all, st) => {
      deployTypes.forEach((dt) => {
        all.push({
          id: this.getServerId(st.id, dt.id),
          content: st.name
        });
      });
      return all;
    }, []));

    // create a dataset with items
    const items = new DataSet();
    items.add({
      id: '1/2_1234',
      group: '1/2',
      content: 'fix/Bugfix/VeryImportantBugfix',
      start: +new Date(),
      end: +new Date() + 1,
      type: 'range'
    });

    const lastWeek = moment().add(-1, 'week').toDate();
    const reservations = await this.reservationService.getReservationsAsync(new ReservationQuery(null, null, null, null));
    for (const reservation of reservations) {
      const instanceId = this.getServerId(reservation.serverType, reservation.deployType);
      items.add({
        id: `${instanceId}_${(Math.random() * 1000000) | 0}`,
        group: instanceId,
        content: reservation.branchName,
        start: +new Date(reservation.reservedInterval.from),
        end: +new Date(reservation.reservedInterval.to),
        type: 'range'
      });
    }

    // create visualization
    const container = document.getElementById('visualization');
    const options = {
      groupOrder: 'content',  // groupOrder can be a property name or a sorting function
      autoResize: true,
      clickToUse: true,
    };

    const timeline = new Timeline(container, items, groups, options);
    timeline.on('click', (properties) => {
      if (['item', 'background'].includes(properties.what) && !properties.group.startsWith('group_')) {
        this.OnCreateReservationAsync(properties);
      }
    });
  }

  private async OnCreateReservationAsync(properties) {
    console.log(properties);

    const [serverType, deployType] = properties.group.split('_');
    const dateFrom = properties.time;
    const dialogRef = this.dialog.open(NewReservationDialogComponent, {
      width: '450px',
      data: new ReservationItem(
        deployType, serverType, dateFrom, null, new DateTimeInterval(null, null), null
      )
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  private getServerId(serverType, deployType) {
    return `${serverType}/${deployType}`;
  }

}
