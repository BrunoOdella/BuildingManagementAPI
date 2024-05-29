import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateConstructionCompanyComponent } from './create-construction-company.component';

describe('CreateConstructionCompanyComponent', () => {
  let component: CreateConstructionCompanyComponent;
  let fixture: ComponentFixture<CreateConstructionCompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateConstructionCompanyComponent]
    });
    fixture = TestBed.createComponent(CreateConstructionCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
