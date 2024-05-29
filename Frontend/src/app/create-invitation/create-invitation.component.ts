import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-invitation',
  templateUrl: './create-invitation.component.html',
  styleUrls: ['./create-invitation.component.css']
})
export class CreateInvitationComponent {
  createInvitationForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  roles = [
    { value: 'manager', label: 'Manager' },
    { value: 'constructioncompanyadmin', label: 'Construction Company Admin' }
  ];

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.createInvitationForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      name: ['', Validators.required],
      expirationDate: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.createInvitationForm.valid) {
      this.http.post('http://localhost:5154/api/v2/invitations', this.createInvitationForm.value).subscribe(
        response => {
          this.successMessage = 'Invitation created successfully!';
          this.errorMessage = '';
        },
        (error) => {
          this.errorMessage = error.error.message;
          this.successMessage = '';
        }
      );
    }
  }

  navigateToDashboard(): void {
    this.router.navigate(['admin-dashboard']);
  }
}
