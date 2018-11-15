//Angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule , ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//PrimeNg
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { InputSwitchModule } from 'primeng/inputswitch';
import {CardModule} from 'primeng/card';


//Routes
import { AppRoutingModule } from './app-routing.module';

//components
import { AppComponent } from './app.component';
import { SigninOidcComponent } from './_components/_security/signin-oidc/signin-oidc.component';
import { HomeComponent } from './_components/home/home.component';
import { RedirectSilentRenewComponent } from './_components/_security/redirect-silent-renew/redirect-silent-renew.component';
import { ProfilesComponent } from './_components/_profile-components/profiles/profiles.component';
import { ProfileDetailComponent } from './_components/_profile-components/profile-detail/profile-detail.component';
import { PatientsComponent } from './_components/_patient-components/patients/patients.component';
import { CreatePatientComponent } from './_components/_patient-components/create-patient/create-patient.component';
import { CreateProfileComponent } from './_components/_profile-components/create-profile/create-profile.component';
import { PatientDetailComponent } from './_components/_patient-components/patient-detail/patient-detail.component';
import { NavbarComponent } from './_components/navbar/navbar.component';

//Interceptor
import { AddAuthorizationHeaderInterceptorProvider } from './_interceptors/add-authorization-header.interceptor';

//Guards
import { AuthGuard } from './_guards/authGuard';

//Services
import { OpenIdConnectService } from './_services/openIdConnect/open-id-connect.service';
import { DataService } from './_services/data/data.service';
import { PatientService } from './_services/patient/patient.service';
import { ProfileService } from './_services/profile/profile.service';
import { ToastService } from './_services/toast/toast.service';

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
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ToastModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    BrowserAnimationsModule,
    InputSwitchModule,
    CardModule
  ],
  providers: [
    AddAuthorizationHeaderInterceptorProvider,
    MessageService,
    AuthGuard,
    OpenIdConnectService,
    DataService,
    PatientService,
    ProfileService,
    ToastService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
