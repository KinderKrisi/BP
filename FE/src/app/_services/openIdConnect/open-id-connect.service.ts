import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ReplaySubject } from 'rxjs';
import { UserManager, User } from 'oidc-client';
import { DataService } from '../data/data.service';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager = new UserManager(environment.openIdConnectSettings);
  private currentUser: User;

  private adminRolesList : string[] = ["Regular", "Super", "Global"]

  userLoaded$ = new ReplaySubject<boolean>(1);

  get userAvailable(): boolean {
    return this.currentUser != null;
  }

  get user(): User {
    return this.currentUser;
  }

  constructor(private dataService : DataService) {
    this.userManager.clearStaleState();

    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log('User loaded.', user);
      }
      this.currentUser = user;
      this.isAdmin();
      this.userLoaded$.next(true);
    });

    this.userManager.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log('User unloaded');
      }
      this.currentUser = null;
      this.isAdmin();
      this.userLoaded$.next(false);
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
      this.dataService.userSetIsAdmin(this.adminRolesList.includes(this.currentUser.profile.role));
  }
}
