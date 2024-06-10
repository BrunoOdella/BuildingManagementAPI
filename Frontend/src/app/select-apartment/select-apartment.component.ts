import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

interface ApartmentResponse {
  apartmentId: string;
  floor: number;
  number: number;
  ownerName: string;
}

@Component({
  selector: 'app-select-apartment',
  templateUrl: './select-apartment.component.html',
  styleUrls: ['./select-apartment.component.css']
})
export class SelectApartmentComponent implements OnInit {
  apartments: ApartmentResponse[] = [];
  buildingId: string = '';

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.buildingId = this.route.snapshot.paramMap.get('buildingId')!;
    this.http.get<ApartmentResponse[]>(`http://localhost:5154/api/v2/buildings/${this.buildingId}/apartments`).subscribe(
      (data) => {
        this.apartments = data.sort((a, b) => a.floor - b.floor);
      },
      (error) => console.error(error)
    );
  }

  selectApartment(apartmentId: string): void {
    this.router.navigate(['add-request', apartmentId]);
  }

  navigateToDashboard(): void {
    this.router.navigate(['manager-dashboard']);
  }
}
