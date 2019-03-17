import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatMenuModule,
  MatIconModule,
  MatToolbarModule,
  MatDatepickerModule,
  MatFormFieldModule,
  MatNativeDateModule,
  MatInputModule, MatDialogModule, MatDividerModule, MatOptionModule, MatSelectModule,
} from '@angular/material';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

import { ReservationComponent } from './page/reservation/reservation.component';
import { FreeResourcesComponent } from './page/freeresources/free-resources.component';
import { BatchReservationComponent } from './page/batchreservation/batch-reservation.component';
import { AdminPanelComponent } from './page/adminpanel/admin-panel.component';
import { LandingPageComponent } from './page/landingpage/landing-page.component';
import { NewReservationDialogComponent } from './dialog/new-reservation/new-reservation-dialog.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModifyReservationComponent } from './dialog/modify-reservation/modify-reservation.component';
import { InfoService } from './service/info.service';
import { ReservationService } from './service/reservation.service';
import { ApiService } from './service/api.service';
import { RouteReuseStrategy } from '@angular/router';
import { CustomReuseStrategy } from './helper/RouteReuseStrategy';

@NgModule({
  declarations: [
    AppComponent,
    ReservationComponent,
    FreeResourcesComponent,
    BatchReservationComponent,
    AdminPanelComponent,
    LandingPageComponent,
    NewReservationDialogComponent,
    ModifyReservationComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatMenuModule,
    MatIconModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatOptionModule,
    MatSelectModule,
    NgxMaterialTimepickerModule,
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: RouteReuseStrategy, useClass: CustomReuseStrategy },
    ApiService,
    InfoService,
    ReservationService,
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    NewReservationDialogComponent
  ]
})
export class AppModule { }
