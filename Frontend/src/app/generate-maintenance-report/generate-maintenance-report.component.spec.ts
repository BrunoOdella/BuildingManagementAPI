import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateMaintenanceReportComponent } from './generate-maintenance-report.component';

describe('GenerateMaintenanceReportComponent', () => {
  let component: GenerateMaintenanceReportComponent;
  let fixture: ComponentFixture<GenerateMaintenanceReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GenerateMaintenanceReportComponent]
    });
    fixture = TestBed.createComponent(GenerateMaintenanceReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
