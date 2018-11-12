import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../_services/profile/profile.service';
import { OpenIdConnectService } from '../../_services/openIdConnect/open-id-connect.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  constructor() { }

  ngOnInit() {

  }

}
