import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface Invitation {
  invitationId: string;
  email: string;
  name: string;
  expirationDate: string;
  status: string;
}

@Component({
  selector: 'app-view-invitations',
  templateUrl: './view-invitations.component.html',
  styleUrls: ['./view-invitations.component.css']
})
export class ViewInvitationsComponent implements OnInit {
  invitations: Invitation[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.http.get<Invitation[]>('http://localhost:5154/api/v2/invitations').subscribe(
      (data) => this.invitations = data,
      (error) => console.error(error)
    );
  }

  deleteInvitation(invitationId: string): void {
    this.http.delete(`http://localhost:5154/api/v2/invitations/${invitationId}`).subscribe(
      response => {
        this.invitations = this.invitations.filter(inv => inv.invitationId !== invitationId);
      },
      (error) => console.error(error)
    );
  }

  navigateToDashboard(): void {
    this.router.navigate(['admin-dashboard']);
  }
}
