import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/authGuard';
import { HomeComponent } from './_components/home/home.component';
import { SigninOidcComponent } from './_components/_security/signin-oidc/signin-oidc.component';
import { RedirectSilentRenewComponent } from './_components/_security/redirect-silent-renew/redirect-silent-renew.component';
import { ProfilesComponent } from './_components/_profile-components/profiles/profiles.component';
import { ProfileDetailComponent } from './_components/_profile-components/profile-detail/profile-detail.component';
import { CreateProfileComponent } from './_components/_profile-components/create-profile/create-profile.component';
import { PatientsComponent } from './_components/_patient-components/patients/patients.component';
import { PatientDetailComponent } from './_components/_patient-components/patient-detail/patient-detail.component';
import { CreatePatientComponent } from './_components/_patient-components/create-patient/create-patient.component';


const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'signin-oidc', component: SigninOidcComponent },
  { path: 'redirect-silentrenew', component: RedirectSilentRenewComponent },
  { path: 'create-profile', component: CreateProfileComponent, canActivate: [AuthGuard]},
  { path: 'profiles', component: ProfilesComponent, canActivate: [AuthGuard]},
  { path: 'profile/:id', component: ProfileDetailComponent, canActivate: [AuthGuard]},
  { path: 'patients', component: PatientsComponent, canActivate: [AuthGuard]},
  { path: 'patient/:id', component: PatientDetailComponent, canActivate: [AuthGuard]},
  { path: 'create-patient', component: CreatePatientComponent, canActivate: [AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
