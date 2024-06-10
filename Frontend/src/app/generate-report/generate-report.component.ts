import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

interface BuildingResponse {
  buildingId: string;
  name: string;
  address: string;
}

interface BuildingReport {
  buildingName: string;
  pendingRequests: number;
  activeRequests: number;
  completedRequests: number;
}

@Component({
  selector: 'app-generate-report',
  templateUrl: './generate-report.component.html',
  styleUrls: ['./generate-report.component.css']
})
export class GenerateReportComponent implements OnInit {
  reportForm: FormGroup;
  buildings: BuildingResponse[] = [];
  reports: BuildingReport[] = [];
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.reportForm = this.fb.group({
      buildingId: ['']
    });
  }

  ngOnInit(): void {
    this.getBuildings();
    this.getReports();
  }

  getBuildings(): void {
    this.http.get<BuildingResponse[]>('http://localhost:5154/api/v2/buildings').subscribe(
      (data) => {
        this.buildings = data;
      },
      (error: HttpErrorResponse) => {
        this.errorMessage = error.error.message;
      }
    );
  }

  getReports(): void {
    const buildingId = this.reportForm.value.buildingId;
    let url = 'http://localhost:5154/api/v2/Reports/request_by_building';
    if (buildingId) {
      url += `?BuildingID=${buildingId}`;
    }
    this.http.get<{ maintenanceStaffReports: BuildingReport[] }>(url).subscribe(
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
