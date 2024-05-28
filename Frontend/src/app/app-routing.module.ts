
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ConstructionCompanyAdminDashboardComponent } from './construction-company-admin-dashboard/construction-company-admin-dashboard.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { MaintenanceStaffDashboardComponent } from './maintenance-staff-dashboard/maintenance-staff-dashboard.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'api/v2/login', component: LoginComponent },
  { path: 'api/v2/admin', component: AdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'api/v2/construction-company-admin', component: ConstructionCompanyAdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },
  { path: 'api/v2/manager', component: ManagerDashboardComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'api/v2/maintenance-staff', component: MaintenanceStaffDashboardComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
  { path: '**', redirectTo: 'api/v2/login' } // Redirigir cualquier ruta no encontrada al login
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
