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
            this.router.navigate(['api/v2/admin']);
            break;
          case 'ConstructionCompanyAdmin':
            this.router.navigate(['api/v2/construction-company-admin']);
            break;
          case 'Manager':
            this.router.navigate(['api/v2/manager']);
            break;
          case 'MaintenanceStaff':
            this.router.navigate(['api/v2/maintenance-staff']);
            break;
        }
      },
      error => {
        this.errorMessage = error;
      }
    );
  }
}
