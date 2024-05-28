import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceStaffDashboardComponent } from './maintenance-staff-dashboard.component';

describe('MaintenanceStaffDashboardComponent', () => {
  let component: MaintenanceStaffDashboardComponent;
  let fixture: ComponentFixture<MaintenanceStaffDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MaintenanceStaffDashboardComponent]
    });
    fixture = TestBed.createComponent(MaintenanceStaffDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
