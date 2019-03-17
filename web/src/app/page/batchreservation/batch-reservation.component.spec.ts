import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchReservationComponent } from './batch-reservation.component';

describe('BatchReservationComponent', () => {
  let component: BatchReservationComponent;
  let fixture: ComponentFixture<BatchReservationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BatchReservationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
