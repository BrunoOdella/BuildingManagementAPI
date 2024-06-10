import { Component } from '@angular/core';
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
  selector: 'app-finalize-request',
  templateUrl: './finalize-request.component.html',
  styleUrls: ['./finalize-request.component.css']
})
export class FinalizeRequestComponent {
  requests: RequestResponse[] = [];
  filteredRequest: RequestResponse[] = [];
  selectedElem: string = '';
  cost: number = -1;

  constructor(private http: HttpClient, private router: Router, private auth: AuthService) {}

  ngOnInit(): void {
    this.http.get<RequestResponse[]>(`${environment.apiUrl}/Requests`).subscribe(
      (data) => this.requests = data.filter(request => request.status === 0),
      (error) => console.error(error)
    );
  }

  navigateToDashboard(): void {
    this.router.navigate(['maintenance-staff-dashboard']);
  }

  selectRequest(Id: string): void {
    this.selectedElem = Id;
  }

  onNumberInputChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const numberValue = parseFloat(input.value);
    //console.log("Current number value: ", numberValue);
    this.cost = numberValue;
  }

  terminateRequest(): void {
    if (this.selectedElem) {
      const url = `${environment.apiUrl}/Requests/${this.selectedElem}/finished`;
      const body = {
        totalCost: this.cost,
        endTime: new Date().toISOString(),
      };

      this.http.put(url, body).subscribe({
        next: (response) => console.log('Request activated', response),
        error: (error) => console.log('Error activating request', error)
      });
    }
  }

}
