import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBuildingsComponent } from './edit-buildings.component';

describe('EditBuildingsComponent', () => {
  let component: EditBuildingsComponent;
  let fixture: ComponentFixture<EditBuildingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBuildingsComponent]
    });
    fixture = TestBed.createComponent(EditBuildingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
