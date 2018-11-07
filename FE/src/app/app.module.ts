import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ToastModule} from 'primeng/toast';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserUpdateComponent } from './user-update/user-update.component';
import { CreateProfileComponent } from './create-profile/create-profile.component';
import { AddAuthorizationHeaderInterceptorProvider } from './_interceptors/add-authorization-header.interceptor';
import { AuthGuard } from './_guards/authGuard';
import { OpenIdConnectService } from './_services/openIdConnect/open-id-connect.service';
import { AuthService } from './_services/_auth/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserUpdateComponent,
    CreateProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ToastModule,
    FormsModule
  ],
  providers: [
    MessageService,
    AddAuthorizationHeaderInterceptorProvider,
    AuthGuard,
    AuthService,
    OpenIdConnectService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
