import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructionCompanyAdminDashboardComponent } from './construction-company-admin-dashboard.component';

describe('ConstructionCompanyAdminDashboardComponent', () => {
  let component: ConstructionCompanyAdminDashboardComponent;
  let fixture: ComponentFixture<ConstructionCompanyAdminDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConstructionCompanyAdminDashboardComponent]
    });
    fixture = TestBed.createComponent(ConstructionCompanyAdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
