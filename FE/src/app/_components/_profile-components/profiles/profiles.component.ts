import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toast/toast.service';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';

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

  constructor(
    private profileService: ProfileService, 
    private router: Router, 
    private toastService: ToastService,
    private userDataService: UserDataService
    ) {
   }


  ngOnInit() {
    this.isAdmin = this.userDataService.getIsAdmin();

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
  deleteProfile(profileId : number) : void {
    this.profileService.deleteProfile(profileId).subscribe();
  }
 deleteProfileAdmin(profileId: number) : void {
  this.profileService.deleteProfileAdmin(profileId).subscribe();   
  }

}
