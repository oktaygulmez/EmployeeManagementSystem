import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { UnAuthorizedComponent } from './pages/un-authorized/un-authorized.component';

import { AuthGuard } from './auth.guard';
import { EmployeeComponent } from './pages/employee/employee.component';
import { DepartmentComponent } from './pages/department/department.component';

const routes: Routes = [
  { path: '', redirectTo: '/Employee', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },

  { path: 'UnAuthorized', component: UnAuthorizedComponent },
  { path: 'NotFound', component: NotFoundComponent },

  { path: 'Employee', component: EmployeeComponent, canActivate: [AuthGuard] },
  { path: 'Department', component: DepartmentComponent, canActivate: [AuthGuard] },

  { path: '**', redirectTo: '/NotFound', pathMatch: 'full' }, // Bilinmeyen yol yönlendirme, yukarıdaki yolların hiçbirini bulamazsa bu çalışacak.
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
