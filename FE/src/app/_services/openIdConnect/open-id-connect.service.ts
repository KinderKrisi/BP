import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ReplaySubject } from 'rxjs';
import { UserManager, User } from 'oidc-client';
import { UserDataService } from '../_data-services/user-data/user-data.service';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
  private currentUser: User;

  private adminRolesList : string[] = ["Regular", "Super", "Global"];

  userLoaded$ = new ReplaySubject<boolean>(1);

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  get user(): User {
    return this.currentUser;
  }

  constructor(private userDataService: UserDataService, private toastService: ToastService) {
    this.userManager.clearStaleState();

    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded.', user);
      }
      this.currentUser = user;
      this.isAdmin();
      this.userLoaded$.next(true);
      this.toastService.toastMessage("success", "Login", "Login successful")
    });

    this.userManager.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log('User unloaded');
      }
      this.currentUser = null;
      this.isAdmin();
      this.userLoaded$.next(false);
      this.toastService.toastMessage("success", "Logout", "Logout successful")
    });
  }

  triggerSignIn() {
    this.userManager.signinRedirect().then(() => {
      if (!environment.production) {
        console.log('Redirection to signin triggered.');
      }
    });
  }

  handleCallback() {
    this.userManager.signinRedirectCallback().then(user => {
      if (!environment.production) {
        console.log('Callback after signin handled.', user);
      }
    });
  }

  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(user => {
      if(user) { 
        this.currentUser = user
        this.isAdmin();
        if (!environment.production) {
          console.log('Callback after silent signin handled.', user);
        }
      }
    });
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then(response => {
      if (!environment.production) {
        console.log('Redirection to sign out triggered.', response);
      }
    });
  };

  private isAdmin() : void{
    if(this.currentUser)
      this.userDataService.setIsAdmin(this.adminRolesList.includes(this.currentUser.profile.role));
  }
}
