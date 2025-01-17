import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  constructor(private router: Router, private authService: AuthService) {}

  navigateToCreateAdmin(): void {
    this.router.navigate(['create-admin']);
  }

  navigateToCreateInvitation(): void {
    this.router.navigate(['create-invitation']);
  }

  navigateToViewInvitations(): void {
    this.router.navigate(['view-invitations']);
  }

  navigateToCreateCategory(): void {
    this.router.navigate(['create-category']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}
