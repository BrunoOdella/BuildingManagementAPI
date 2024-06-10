import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-accept-invitation',
  templateUrl: './accept-invitation.component.html',
  styleUrls: ['./accept-invitation.component.css']
})
export class AcceptInvitationComponent {
  acceptInvitationForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.acceptInvitationForm = this.fb.group({
      invitationId: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.acceptInvitationForm.valid) {
      const { invitationId, email, password } = this.acceptInvitationForm.value;
      this.http.put(`${environment.apiUrl}/invitations/${invitationId}`, { email, password }).subscribe(
        response => {
          this.successMessage = 'Invitation accepted successfully!';
          this.errorMessage = '';
        },
        (error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          this.successMessage = '';
        }
      );
    }
  }

  navigateToLogin(): void {
    this.router.navigate(['login']);
  }
}
