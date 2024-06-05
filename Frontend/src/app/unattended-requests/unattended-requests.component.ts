import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

interface RequestResponse {
  id: string;
  apartmentId: string;
  category: string;
  description: string;
  creationTime: string;
  status: number;
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
    this.http.get<RequestResponse[]>('http://localhost:5154/api/v2/Requests').subscribe(
      (data) => this.requests = data,
      (error) => console.error(error)
    );
    
  }

  selectRequest(Id: string): void {
    this.selectedElem = Id;
    console.log(this.selectedElem);
    this.requests = this.requests.filter(request => request.status === 2);
    console.log(this.requests);
  }

  navigateToDashboard(): void {
    this.router.navigate(['maintenance-staff-dashboard']);
  }

  activateRequest(): void {
    if (this.selectedElem) {
      const url = `http://localhost:5154/api/v2/Requests/${this.selectedElem}`;
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
