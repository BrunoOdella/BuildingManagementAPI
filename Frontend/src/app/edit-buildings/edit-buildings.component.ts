import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';

interface BuildingResponse {
  buildingId: string;
  name: string;
  address: string;
  latitude: number;
  longitude: number;
  commonExpenses: number;
}

@Component({
  selector: 'app-edit-buildings',
  templateUrl: './edit-buildings.component.html',
  styleUrls: ['./edit-buildings.component.css']
})
export class EditBuildingsComponent implements OnInit {
  buildings: BuildingResponse[] = [];
  selectedBuilding: BuildingResponse | null = null;
  editBuildingForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.editBuildingForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],
      commonExpenses: ['', Validators.required],
      managerGuid: ['00000000-0000-0000-0000-000000000000']
    });
  }

  ngOnInit(): void {
    this.getBuildings();
  }

  getBuildings(): void {
    this.http.get<BuildingResponse[]>(`${environment.apiUrl}/buildings`).subscribe(
      (data) => {
        this.buildings = data;
      },
      (error: HttpErrorResponse) => {
        this.errorMessage = error.error.message;
      }
    );
  }

  selectBuilding(building: BuildingResponse): void {
    this.selectedBuilding = building;
    this.editBuildingForm.patchValue({
      name: building.name,
      address: building.address,
      latitude: building.latitude,
      longitude: building.longitude,
      commonExpenses: building.commonExpenses
    });
  }

  onSubmit(): void {
    if (this.editBuildingForm.valid && this.selectedBuilding) {
      const updatePayload = this.editBuildingForm.value;
      const buildingId = this.selectedBuilding.buildingId;

      this.http.put(`http://localhost:5154/api/v2/buildings/${buildingId}`, updatePayload).subscribe(
        () => {
          this.successMessage = 'Building updated successfully!';
          this.errorMessage = '';
          this.getBuildings();
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
