import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-maintenance-staff-dashboard',
  templateUrl: './maintenance-staff-dashboard.component.html',
  styleUrls: ['./maintenance-staff-dashboard.component.css']
})
export class MaintenanceStaffDashboardComponent {
  constructor(private router: Router, private authService: AuthService) {}

  navigateToUnattendedRequests(): void {
    this.router.navigate(['unattended-requests']);
  }

  navigateToFinalizeRequest(): void {
    this.router.navigate(['finalize-request']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}
