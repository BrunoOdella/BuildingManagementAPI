import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';

interface CategoryResponse {
  categoryID: number;
  name: string;
  description: string;
}

interface MaintenanceStaffResponse {
  id: string;
  name: string;
  lastName: string;
  email: string;
}

@Component({
  selector: 'app-add-request',
  templateUrl: './add-request.component.html',
  styleUrls: ['./add-request.component.css']
})
export class AddRequestComponent implements OnInit {
  addRequestForm: FormGroup;
  apartmentId: string = '';
  categories: CategoryResponse[] = [];
  maintenanceStaff: MaintenanceStaffResponse[] = [];
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.addRequestForm = this.fb.group({
      description: ['', Validators.required],
      category: ['', Validators.required],
      maintenanceStaff: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.apartmentId = this.route.snapshot.paramMap.get('apartmentId')!;

    this.http.get<CategoryResponse[]>(`${environment.apiUrl}/categoriesrequests`).subscribe(
      (data) => this.categories = data,
      (error) => console.error(error)
    );

    this.http.get<MaintenanceStaffResponse[]>(`${environment.apiUrl}/maintenancestaff`).subscribe(
      (data) => this.maintenanceStaff = data,
      (error) => console.error(error)
    );
  }

  onSubmit(): void {
    if (this.addRequestForm.valid) {
      const requestPayload = {
        description: this.addRequestForm.value.description,
        category: this.addRequestForm.value.category,
        creationTime: new Date().toISOString(),
        apartmentID: this.apartmentId,
        maintenanceStaffID: this.addRequestForm.value.maintenanceStaff
      };

      this.http.post(`${environment.apiUrl}/requests`, requestPayload).subscribe(
        response => {
          this.successMessage = 'Request added successfully!';
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
