import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-construction-company',
  templateUrl: './create-construction-company.component.html',
  styleUrls: ['./create-construction-company.component.css']
})
export class CreateConstructionCompanyComponent {
  createCompanyForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.createCompanyForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.createCompanyForm.valid) {
      this.http.post<{ companyId: string }>('http://localhost:5154/api/v2/ConstructionCompany', this.createCompanyForm.value).subscribe(
        response => {
          this.successMessage = 'Construction Company created successfully!';
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
