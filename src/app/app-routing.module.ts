import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LandingComponent } from './pages/landing/landing.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UploadResumeComponent } from './pages/upload-resume/upload-resume.component';
import { ResultComponent } from './pages/result/result.component';

const routes: Routes = [

  { path: '', component: LandingComponent },

  { path: 'login', component: LoginComponent },

  { path: 'register', component: RegisterComponent },

  { path: 'dashboard', component: DashboardComponent },

  { path: 'upload', component: UploadResumeComponent },

  { path: 'result', component: ResultComponent },

  { path: '**', redirectTo: '' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }