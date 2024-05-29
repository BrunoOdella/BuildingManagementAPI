// src/app/login/login.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authService.login({ email: this.email, password: this.password }).subscribe(
      response => {
        switch (response.userType) {
          case 'Admin':
            this.router.navigate(['admin-dashboard']);
            break;
          case 'ConstructionCompanyAdmin':
            this.router.navigate(['construction-company-admin-dashboard']);
            break;
          case 'Manager':
            this.router.navigate(['manager-dashboard']);
            break;
          case 'MaintenanceStaff':
            this.router.navigate(['maintenance-staff-dashboard']);
            break;
        }
      },
      error => {
        this.errorMessage = error;
      }
    );
  }
}
