// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ConstructionCompanyAdminDashboardComponent } from './construction-company-admin-dashboard/construction-company-admin-dashboard.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { MaintenanceStaffDashboardComponent } from './maintenance-staff-dashboard/maintenance-staff-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { CreateAdminComponent } from './create-admin/create-admin.component';
import { CreateInvitationComponent } from './create-invitation/create-invitation.component';
import { ViewInvitationsComponent } from './view-invitations/view-invitations.component';
import { CreateCategoryComponent } from './create-category/create-category.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-admin', component: CreateAdminComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-invitation', component: CreateInvitationComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'view-invitations', component: ViewInvitationsComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-category', component: CreateCategoryComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },

  { path: 'construction-company-admin-dashboard', component: ConstructionCompanyAdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },
  { path: 'manager-dashboard', component: ManagerDashboardComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'maintenance-staff-dashboard', component: MaintenanceStaffDashboardComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
  { path: '**', redirectTo: 'login' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
