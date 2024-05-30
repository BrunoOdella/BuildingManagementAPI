import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-maintenance-staff',
  templateUrl: './create-maintenance-staff.component.html',
  styleUrls: ['./create-maintenance-staff.component.css']
})
export class CreateMaintenanceStaffComponent {
  createMaintenanceStaffForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.createMaintenanceStaffForm = this.fb.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.createMaintenanceStaffForm.valid) {
      this.http.post('http://localhost:5154/api/v2/maintenancestaff', this.createMaintenanceStaffForm.value).subscribe(
        response => {
          this.successMessage = 'Maintenance staff created successfully!';
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
    this.router.navigate(['manager-dashboard']);
  }
}
