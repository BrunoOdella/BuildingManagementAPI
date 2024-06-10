import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-create-building',
  templateUrl: './create-building.component.html',
  styleUrls: ['./create-building.component.css']
})
export class CreateBuildingComponent {
  createBuildingForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.createBuildingForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      latitude: ['', [Validators.required, Validators.pattern(/^(-?\d+(\.\d+)?)$/)]],
      longitude: ['', [Validators.required, Validators.pattern(/^(-?\d+(\.\d+)?)$/)]],
      commonExpenses: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      apartments: this.fb.array([])
    });
  }

  get apartments(): FormArray {
    return this.createBuildingForm.get('apartments') as FormArray;
  }

  addApartment(): void {
    this.apartments.push(this.fb.group({
      floor: ['', Validators.required],
      number: ['', Validators.required],
      owner: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]]
      }),
      numberOfBathrooms: ['', Validators.required],
      hasTerrace: [false]
    }));
  }

  removeApartment(index: number): void {
    this.apartments.removeAt(index);
  }

  onSubmit(): void {
    if (this.createBuildingForm.valid) {
      this.http.post(`${environment.apiUrl}/buildings`, this.createBuildingForm.value).subscribe(
        response => {
          this.successMessage = 'Building created successfully!';
          this.errorMessage = '';
        },
        (error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          this.successMessage = '';
        }
      );
    }
  }

  navigateToDashboard(): void {
    this.router.navigate(['construction-company-admin-dashboard']);
  }
}
