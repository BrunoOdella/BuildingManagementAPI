import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnattendedRequestsComponent } from './unattended-requests.component';

describe('UnattendedRequestsComponent', () => {
  let component: UnattendedRequestsComponent;
  let fixture: ComponentFixture<UnattendedRequestsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UnattendedRequestsComponent]
    });
    fixture = TestBed.createComponent(UnattendedRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
