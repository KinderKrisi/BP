import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { DataService } from 'src/app/_services/data/data.service';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.scss']
})
export class ProfileDetailComponent implements OnInit {

  profileId : number;
  profile: HospitalProfile;
  isEditingEnabled: boolean = false;

  constructor(private profileService: ProfileService, private activatedRoute : ActivatedRoute, private dataService: DataService) {
    this.activatedRoute.params.subscribe( x => this.profileId = x.id);
    this.profile = this.dataService.profileFindProfileInList(this.profileId);
   }

  ngOnInit() {
    console.log(this.profile)
  }

}
