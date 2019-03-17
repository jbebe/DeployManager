import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReservationComponent } from './page/reservation/reservation.component';
import { FreeResourcesComponent } from './page/freeresources/free-resources.component';
import { BatchReservationComponent } from './page/batchreservation/batch-reservation.component';
import { AdminPanelComponent } from './page/adminpanel/admin-panel.component';
import { LandingPageComponent } from './page/landingpage/landing-page.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'reservation', component: ReservationComponent },
  { path: 'free-resources', component: FreeResourcesComponent },
  { path: 'batch-reservation', component: BatchReservationComponent },
  { path: 'admin-panel', component: AdminPanelComponent },
  { path: '*', component: LandingPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
