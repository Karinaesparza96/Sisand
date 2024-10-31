import { Routes } from '@angular/router';
import { LoginComponent } from '../components/login/login.component';
import { RegisterComponent } from '../components/register/register.component';
import { UserListComponent } from '../components/user-list/user-list.component';
import { UserFormComponent } from '../components/user-form/user-form.component';
import { AuthGuard } from '../guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'usuarios', component: UserListComponent, canActivate: [AuthGuard] },
  { path: 'usuarios/criar', component: UserFormComponent, canActivate: [AuthGuard] }, 
  { path: 'usuarios/editar/:id', component: UserFormComponent, canActivate: [AuthGuard] }, 
  { path: '', redirectTo: '/usuarios', pathMatch: 'full' },
];
