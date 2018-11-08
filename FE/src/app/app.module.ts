import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ToastModule} from 'primeng/toast';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { UserUpdateComponent } from './user-update/user-update.component';
import { CreateProfileComponent } from './create-profile/create-profile.component';
import { AddAuthorizationHeaderInterceptorProvider } from './_interceptors/add-authorization-header.interceptor';
import { AuthGuard } from './_guards/authGuard';
import { OpenIdConnectService } from './_services/openIdConnect/open-id-connect.service';
import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';
import { HomeComponent } from './home/home.component';
import { RedirectSilentRenewComponent } from './redirect-silent-renew/redirect-silent-renew.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    UserUpdateComponent,
    CreateProfileComponent,
    SigninOidcComponent,
    HomeComponent,
    RedirectSilentRenewComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ToastModule,
    FormsModule
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
