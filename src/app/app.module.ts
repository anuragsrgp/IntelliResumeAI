import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './pages/landing/landing.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UploadResumeComponent } from './pages/upload-resume/upload-resume.component';
import { ResultComponent } from './pages/result/result.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './shared/navbar/navbar.component';
@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    LoginComponent,
    RegisterComponent,
    DashboardComponent,
    UploadResumeComponent,
    ResultComponent,
    NavbarComponent
  ],
imports: [
  BrowserModule,
  FormsModule,
  HttpClientModule,
  AppRoutingModule
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
