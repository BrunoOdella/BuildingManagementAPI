import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportBuildingsComponent } from './import-buildings.component';

describe('ImportBuildingsComponent', () => {
  let component: ImportBuildingsComponent;
  let fixture: ComponentFixture<ImportBuildingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImportBuildingsComponent]
    });
    fixture = TestBed.createComponent(ImportBuildingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
