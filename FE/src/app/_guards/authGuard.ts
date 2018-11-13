import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { OpenIdConnectService } from "../_services/openIdConnect/open-id-connect.service";

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private openIdConnectService: OpenIdConnectService) {

  }

  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.openIdConnectService.userAvailable) {
      return true;
    }
    else {
      // trigger signin
      this.openIdConnectService.triggerSignIn();
      return false;
    }
  }
}