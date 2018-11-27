import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from 'src/app/_services/openIdConnect/open-id-connect.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean = false;
  constructor(private openIdConnectService: OpenIdConnectService) { }

  ngOnInit() {
    this.isLoggedIn = this.openIdConnectService.userAvailable;
  }

  logout() {
    console.log("safsdfs")
    this.openIdConnectService.triggerSignOut();
  }
}
