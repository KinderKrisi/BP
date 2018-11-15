import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from 'src/app/_services/openIdConnect/open-id-connect.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean = false;
  items : MenuItem[];
  constructor(private openIdConnectService: OpenIdConnectService) { }

  ngOnInit() {
    this.isLoggedIn = this.openIdConnectService.userAvailable;
  }

  logout() {
    this.openIdConnectService.triggerSignOut();
  }
}
