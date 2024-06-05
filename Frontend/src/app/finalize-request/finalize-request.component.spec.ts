import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinalizeRequestComponent } from './finalize-request.component';

describe('FinalizeRequestComponent', () => {
  let component: FinalizeRequestComponent;
  let fixture: ComponentFixture<FinalizeRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FinalizeRequestComponent]
    });
    fixture = TestBed.createComponent(FinalizeRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
