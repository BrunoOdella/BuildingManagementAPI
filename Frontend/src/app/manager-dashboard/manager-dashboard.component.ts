import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-manager-dashboard',
  templateUrl: './manager-dashboard.component.html',
  styleUrls: ['./manager-dashboard.component.css']
})
export class ManagerDashboardComponent {
  constructor(private router: Router, private authService: AuthService) {}

  navigateToCreateMaintenanceStaff(): void {
    this.router.navigate(['create-maintenance-staff']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}
