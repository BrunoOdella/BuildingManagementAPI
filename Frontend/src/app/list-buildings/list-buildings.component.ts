import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface BuildingResponse {
  buildingId: string;
  name: string;
  address: string;
  latitude: number;
  longitude: number;
  constructionCompanyName: string;
  commonExpenses: number;
  managerName: string | null;
}

@Component({
  selector: 'app-list-buildings',
  templateUrl: './list-buildings.component.html',
  styleUrls: ['./list-buildings.component.css']
})
export class ListBuildingsComponent implements OnInit {
  buildings: BuildingResponse[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.http.get<BuildingResponse[]>('http://localhost:5154/api/v2/buildings').subscribe(
      (data) => this.buildings = data,
      (error) => console.error(error)
    );
  }

  navigateToDashboard(): void {
    this.router.navigate(['construction-company-admin-dashboard']);
  }
}
