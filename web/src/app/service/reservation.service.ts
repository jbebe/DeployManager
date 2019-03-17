import { Injectable } from '@angular/core';
import { ReservationItem } from '../entity/ReservationItem';
import { ApiService } from './api.service';
import { ReservationQuery } from '../entity/ReservationQuery';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(
    private api: ApiService
  ) { }

  public async createReservationAsync(reservationItem: ReservationItem) {
    await this.api.post<any>('reservation', reservationItem);
  }

  public async getReservationsAsync(query: ReservationQuery): Promise<ReservationItem[]> {
    return await this.api.get<ReservationItem[]>('reservation', query);
  }
}
