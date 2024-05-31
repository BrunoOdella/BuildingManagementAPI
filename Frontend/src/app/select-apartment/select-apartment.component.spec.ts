import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectApartmentComponent } from './select-apartment.component';

describe('SelectApartmentComponent', () => {
  let component: SelectApartmentComponent;
  let fixture: ComponentFixture<SelectApartmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectApartmentComponent]
    });
    fixture = TestBed.createComponent(SelectApartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
