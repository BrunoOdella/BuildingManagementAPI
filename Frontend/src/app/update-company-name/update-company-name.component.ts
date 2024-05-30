import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-company-name',
  templateUrl: './update-company-name.component.html',
  styleUrls: ['./update-company-name.component.css']
})
export class UpdateCompanyNameComponent {
  updateCompanyNameForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.updateCompanyNameForm = this.fb.group({
      newName: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.updateCompanyNameForm.valid) {
      this.http.put('http://localhost:5154/api/v2/ConstructionCompany', this.updateCompanyNameForm.value).subscribe(
        response => {
          this.successMessage = 'Company name updated successfully!';
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
