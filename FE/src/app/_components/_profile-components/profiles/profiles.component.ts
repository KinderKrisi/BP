import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { OpenIdConnectService } from 'src/app/_services/openIdConnect/open-id-connect.service';
import { DataService } from 'src/app/_services/data/data.service';
import { ToastService } from 'src/app/_services/toast/toast.service';

@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.scss']
})
export class ProfilesComponent implements OnInit {

  userProfileList : HospitalProfile[] = [];
  adminProfileList : HospitalProfile[] = [];
  profiles : Observable<HospitalProfile[]>;
  isAdmin : boolean;

  constructor(private profileService: ProfileService, private router: Router, private dataService : DataService, private toastService: ToastService) {
   }


  ngOnInit() {
    this.isAdmin = this.dataService.userGetIsAdmin();

    this.profileService.getProfilesForUser().subscribe(x => this.userProfileList = x)
    if(this.isAdmin)this.profileService.getAllProfilesAdmin().subscribe(x => this.adminProfileList = x)
  }

  getUsersProfiles() : void {
    this.profileService.getProfilesForUser().subscribe(x =>{
       this.userProfileList = x;
       this.toastService.toastMessage("success", "Refresh Profiles", "Profile list has been refreshed")
      })
  }

  getAllProfiles() : void {
    if(this.isAdmin)this.profileService.getAllProfilesAdmin().subscribe(x => {
      this.adminProfileList = x
      this.toastService.toastMessage("success", "Refresh All Profiles", "Admin profiles has been refreshed")
    })
  }
  navigateToDetail(id: number) : void {
    this.router.navigate(['/profile', id]);
    console.log("navigated to detail")
  }

}
