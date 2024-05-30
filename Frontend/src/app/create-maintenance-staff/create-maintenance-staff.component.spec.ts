import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMaintenanceStaffComponent } from './create-maintenance-staff.component';

describe('CreateMaintenanceStaffComponent', () => {
  let component: CreateMaintenanceStaffComponent;
  let fixture: ComponentFixture<CreateMaintenanceStaffComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateMaintenanceStaffComponent]
    });
    fixture = TestBed.createComponent(CreateMaintenanceStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
