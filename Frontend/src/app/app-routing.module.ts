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
import { CreateConstructionCompanyComponent } from './create-construction-company/create-construction-company.component';
import { AcceptInvitationComponent } from './accept-invitation/accept-invitation.component';
import { CreateBuildingComponent } from './create-building/create-building.component';
import { ListBuildingsComponent } from './list-buildings/list-buildings.component';
import { UpdateCompanyNameComponent } from './update-company-name/update-company-name.component';
import { CreateMaintenanceStaffComponent } from './create-maintenance-staff/create-maintenance-staff.component';
import { SelectBuildingComponent } from './select-building/select-building.component';
import { SelectApartmentComponent } from './select-apartment/select-apartment.component';
import { AddRequestComponent } from './add-request/add-request.component';
import { UnattendedRequestsComponent } from './unattended-requests/unattended-requests.component'
import { FinalizeRequestComponent } from './finalize-request/finalize-request.component';
import { ImportBuildingsComponent } from './import-buildings/import-buildings.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'accept-invitation', component: AcceptInvitationComponent },

  { path: 'admin-dashboard', component: AdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-admin', component: CreateAdminComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-invitation', component: CreateInvitationComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'view-invitations', component: ViewInvitationsComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'create-category', component: CreateCategoryComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },

  { path: 'construction-company-admin-dashboard', component: ConstructionCompanyAdminDashboardComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },
  { path: 'create-construction-company', component: CreateConstructionCompanyComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' }},
  { path: 'create-building', component: CreateBuildingComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' }},
  {path: 'list-buildings', component: ListBuildingsComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' }},
  { path: 'update-company-name', component: UpdateCompanyNameComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },
  { path: 'import-buildings', component: ImportBuildingsComponent, canActivate: [AuthGuard], data: { role: 'ConstructionCompanyAdmin' } },

  { path: 'manager-dashboard', component: ManagerDashboardComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'create-maintenance-staff', component: CreateMaintenanceStaffComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'select-building', component: SelectBuildingComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'select-apartment/:buildingId', component: SelectApartmentComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
  { path: 'add-request/:apartmentId', component: AddRequestComponent, canActivate: [AuthGuard], data: { role: 'Manager' } },
 
  { path: 'maintenance-staff-dashboard', component: MaintenanceStaffDashboardComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
  { path: 'unattended-requests', component: UnattendedRequestsComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
  { path: 'finalize-request', component: FinalizeRequestComponent, canActivate: [AuthGuard], data: { role: 'MaintenanceStaff' } },
 
  { path: '**', redirectTo: 'login' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
