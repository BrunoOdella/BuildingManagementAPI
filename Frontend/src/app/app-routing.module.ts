import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ConstructionCompanyAdminDashboardComponent } from './construction-company-admin-dashboard/construction-company-admin-dashboard.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { MaintenanceStaffDashboardComponent } from './maintenance-staff-dashboard/maintenance-staff-dashboard.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'admin', component: AdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'construction-company-admin', component: ConstructionCompanyAdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },
  { path: 'manager', component: ManagerDashboardComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'maintenance-staff', component: MaintenanceStaffDashboardComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
