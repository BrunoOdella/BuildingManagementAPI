import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-construction-company-admin-dashboard',
  templateUrl: './construction-company-admin-dashboard.component.html',
  styleUrls: ['./construction-company-admin-dashboard.component.css']
})
export class ConstructionCompanyAdminDashboardComponent {
  constructor(private router: Router, private authService: AuthService) {}

  navigateToCreateCompany(): void {
    this.router.navigate(['create-construction-company']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}