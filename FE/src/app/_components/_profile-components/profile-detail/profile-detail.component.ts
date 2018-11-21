import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { ProfileDataService } from 'src/app/_services/_data-services/profile-data/profile-data.service';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';
import { ProfileService } from 'src/app/_services/profile/profile.service';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.scss']
})
export class ProfileDetailComponent implements OnInit {

  profileId : number;
  profile: HospitalProfile;
  isEditingEnabled: boolean = false;
  isAdmin : boolean = false;

  constructor(
    private activatedRoute : ActivatedRoute,
    private profileDataService: ProfileDataService,
    private userDataService: UserDataService,
    private profileService: ProfileService,
    private router: Router
    ) {

   }

  ngOnInit() {
    this.activatedRoute.params.subscribe( x => this.profileId = x.id);
    this.profile = this.profileDataService.findProfileInList(this.profileId);
    console.log(this.profile);
    this.isAdmin = this.userDataService.getIsAdmin();
  }
  deleteProfile() : void {
    this.profileService.deleteProfile(this.profile.id).subscribe( () => this.router.navigate(["/home"]));
  }

}
