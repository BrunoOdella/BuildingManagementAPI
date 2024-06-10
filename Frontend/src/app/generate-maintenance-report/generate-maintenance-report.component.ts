// src/app/generate-maintenance-report/generate-maintenance-report.component.ts
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

interface MaintenanceStaffResponse {
  id: string;
  name: string;
  lastName: string;
  email: string;
}

interface MaintenanceStaffReport {
  staffName: string;
  pendingRequests: number;
  activeRequests: number;
  completedRequests: number;
  averageCompletionTimeInHours: number;
}

@Component({
  selector: 'app-generate-maintenance-report',
  templateUrl: './generate-maintenance-report.component.html',
  styleUrls: ['./generate-maintenance-report.component.css']
})
export class GenerateMaintenanceReportComponent implements OnInit {
  reportForm: FormGroup;
  maintenanceStaff: MaintenanceStaffResponse[] = [];
  reports: MaintenanceStaffReport[] = [];
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.reportForm = this.fb.group({
      maintenanceStaffId: ['']
    });
  }

  ngOnInit(): void {
    this.getMaintenanceStaff();
    this.getReports();
  }

  getMaintenanceStaff(): void {
    this.http.get<MaintenanceStaffResponse[]>(`${environment.apiUrl}/maintenancestaff`).subscribe(
      (data) => {
        this.maintenanceStaff = data;
      },
      (error: HttpErrorResponse) => {
        this.errorMessage = error.error.message;
      }
    );
  }

  getReports(): void {
    const maintenanceStaffId = this.reportForm.value.maintenanceStaffId;
    let url = `${environment.apiUrl}/Reports/request_by_maintenance_staff`;
    if (maintenanceStaffId) {
      url += `?MaintenanceStaffID=${maintenanceStaffId}`;
    }
    this.http.get<{ maintenanceStaffReports: MaintenanceStaffReport[] }>(url).subscribe(
      (data) => {
        this.reports = data.maintenanceStaffReports;
        this.errorMessage = '';
      },
      (error: HttpErrorResponse) => {
        this.errorMessage = error.error.message;
      }
    );
  }

  onSubmit(): void {
    this.getReports();
  }

  navigateToDashboard(): void {
    this.router.navigate(['manager-dashboard']);
  }
}
