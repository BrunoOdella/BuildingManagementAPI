import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { environment } from 'src/environments/environment';

interface RequestResponse {
  id: string; 
  description: string;
  status: number; 
  category: number | null; 
  creationTime: string; 
  startTime: string; 
  endTime: string; 
  totalCost: number; 
  apartmentId: string; 
}

@Component({
  selector: 'app-unattended-requests',
  templateUrl: './unattended-requests.component.html',
  styleUrls: ['./unattended-requests.component.css']
})
export class UnattendedRequestsComponent {
  requests: RequestResponse[] = [];
  selectedElem: string = '';
  

  constructor(private http: HttpClient, private router: Router, private auth: AuthService) {}

 ngOnInit(): void {
    this.http.get<RequestResponse[]>(`${environment.apiUrl}/Requests`).subscribe(
      (data) => this.requests = data.filter(request => request.status === 2),
      (error) => console.error(error)
    );
  }

  selectRequest(Id: string): void {
    this.selectedElem = Id;
  }

  navigateToDashboard(): void {
    this.router.navigate(['maintenance-staff-dashboard']);
  }

  activateRequest(): void {
    if (this.selectedElem) {
      const url = `${environment.apiUrl}/Requests/${this.selectedElem}`;
      const body = {
        status: 0,
        startTime: new Date().toISOString(),
        maintenancePersonId: this.auth.getAuthHeaders().get('Authorization')
      };

      this.http.put(url, body).subscribe({
        next: (response) => console.log('Request activated', response),
        error: (error) => console.log('Error activating request', error)
      });
    }
  }
}
