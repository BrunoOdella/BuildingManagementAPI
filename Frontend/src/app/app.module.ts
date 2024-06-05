// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { ConstructionCompanyAdminDashboardComponent } from './construction-company-admin-dashboard/construction-company-admin-dashboard.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { MaintenanceStaffDashboardComponent } from './maintenance-staff-dashboard/maintenance-staff-dashboard.component';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './guards/auth.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { ToggleThemeComponent } from './toggle-theme/toggle-theme.component';
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
import { ImportBuildingsComponent } from './import-buildings/import-buildings.component';
import { UnattendedRequestsComponent } from './unattended-requests/unattended-requests.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FinalizeRequestComponent } from './finalize-request/finalize-request.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminDashboardComponent,
    ConstructionCompanyAdminDashboardComponent,
    ManagerDashboardComponent,
    MaintenanceStaffDashboardComponent,
    ToggleThemeComponent,
    CreateAdminComponent,
    CreateInvitationComponent,
    ViewInvitationsComponent,
    CreateCategoryComponent,
    CreateConstructionCompanyComponent,
    AcceptInvitationComponent,
    CreateBuildingComponent,
    ListBuildingsComponent,
    UpdateCompanyNameComponent,
    CreateMaintenanceStaffComponent,
    SelectBuildingComponent,
    SelectApartmentComponent,
    AddRequestComponent,
    ImportBuildingsComponent,
    UnattendedRequestsComponent,
    FinalizeRequestComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
