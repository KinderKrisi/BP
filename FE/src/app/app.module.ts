import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { FormsModule , ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddAuthorizationHeaderInterceptorProvider } from './_interceptors/add-authorization-header.interceptor';
import { AuthGuard } from './_guards/authGuard';
import { OpenIdConnectService } from './_services/openIdConnect/open-id-connect.service';
import { SigninOidcComponent } from './_components/_security/signin-oidc/signin-oidc.component';
import { HomeComponent } from './_components/home/home.component';
import { RedirectSilentRenewComponent } from './_components/_security/redirect-silent-renew/redirect-silent-renew.component';
import { ProfilesComponent } from './_components/_profile-components/profiles/profiles.component';
import { ProfileDetailComponent } from './_components/_profile-components/profile-detail/profile-detail.component';
import { PatientsComponent } from './_components/_patient-components/patients/patients.component';
import { CreatePatientComponent } from './_components/_patient-components/create-patient/create-patient.component';
import { CreateProfileComponent } from './_components/_profile-components/create-profile/create-profile.component';
import { PatientDetailComponent } from './_components/_patient-components/patient-detail/patient-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateProfileComponent,
    SigninOidcComponent,
    HomeComponent,
    RedirectSilentRenewComponent,
    ProfilesComponent,
    ProfileDetailComponent,
    PatientsComponent,
    CreatePatientComponent,
    PatientDetailComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ToastModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule
  ],
  providers: [
    AddAuthorizationHeaderInterceptorProvider,
    MessageService,
    AuthGuard,
    OpenIdConnectService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
