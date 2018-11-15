import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../_services/profile/profile.service';
import { OpenIdConnectService } from '../../_services/openIdConnect/open-id-connect.service';
import { PatientService } from 'src/app/_services/patient/patient.service';
import { Patient } from 'src/app/_models/patient';
import { DataService } from 'src/app/_services/data/data.service';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {


  isAdmin : boolean = false;



  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.isAdmin = this.dataService.userGetIsAdmin();
  }
}
