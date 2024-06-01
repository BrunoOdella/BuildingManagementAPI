import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';

interface BuildingResponse {
  buildingId: string;
  name: string;
  address: string;
  latitude: number;
  longitude: number;
  constructionCompanyName: string;
  commonExpenses: number;
  managerName: string;
}

interface ManagerResponse {
  managerId: string;
  email: string;
}

@Component({
  selector: 'app-list-buildings',
  templateUrl: './list-buildings.component.html',
  styleUrls: ['./list-buildings.component.css']
})
export class ListBuildingsComponent implements OnInit {
  buildings: BuildingResponse[] = [];
  managers: ManagerResponse[] = [];
  managerForms: { [buildingId: string]: FormGroup } = {};

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.http.get<BuildingResponse[]>('http://localhost:5154/api/v2/buildings').subscribe(
      (data) => {
        this.buildings = data;
        this.buildings.forEach(building => {
          if (building.managerName === 'No Manager Assigned') {
            this.managerForms[building.buildingId] = this.fb.group({
              manager: new FormControl('')
            });
          }
        });
      },
      (error) => console.error(error)
    );

    this.http.get<ManagerResponse[]>('http://localhost:5154/api/v2/managers').subscribe(
      (data) => {
        this.managers = data;
        console.log('Managers:', this.managers);
      },
      (error) => console.error(error)
    );
  }

  updateBuilding(building: BuildingResponse): void {
    const selectedManagerId = this.managerForms[building.buildingId].get('manager')?.value;

    const updateRequest = {
      name: building.name,
      address: building.address,
      latitude: building.latitude,
      longitude: building.longitude,
      commonExpenses: building.commonExpenses,
      managerGuid: selectedManagerId
    };

    this.http.put(`http://localhost:5154/api/v2/buildings/${building.buildingId}`, updateRequest).subscribe(
      response => {
        console.log('Building updated successfully');
        building.managerName = this.managers.find(manager => manager.managerId === selectedManagerId)?.email || 'Assigned';
      },
      error => console.error(error)
    );
  }

  navigateToDashboard(): void {
    this.router.navigate(['construction-company-admin-dashboard']);
  }
}
