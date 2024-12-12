import { Routes } from '@angular/router';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { AuthGuard } from './_guards/auth.guard';

export const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  {
    path: 'register-user',
    loadComponent: () => import("./login/register/user/register-user.component").then(m => m.RegisterUserComponent)
  },
  {
    path: 'register-organization',
    loadComponent: () => import("./login/register/organization/register-organization.component").then(m => m.RegisterOrganizationComponent)
  }
];