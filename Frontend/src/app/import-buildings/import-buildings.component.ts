import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-import-buildings',
  templateUrl: './import-buildings.component.html',
  styleUrls: ['./import-buildings.component.css']
})
export class ImportBuildingsComponent {
  importBuildingsForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.importBuildingsForm = this.fb.group({
      assemblyPath: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.importBuildingsForm.valid) {
      const requestPayload = {
        assemblyPath: this.importBuildingsForm.value.assemblyPath
      };

      this.http.post('http://localhost:5154/api/v2/ImportBuildings', requestPayload).subscribe(
        response => {
          this.successMessage = 'Buildings Created';
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
