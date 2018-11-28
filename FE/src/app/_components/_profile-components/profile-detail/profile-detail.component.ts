import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { ProfileDataService } from 'src/app/_services/_data-services/profile-data/profile-data.service';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { ToastService } from 'src/app/_services/toast/toast.service';
import { LogVM } from 'src/app/_models/_viewModels/logVM';
import { LogsendService } from 'src/app/_services/logSend/logsend.service';

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
    private router: Router,
    private toastService : ToastService,
    private logService : LogsendService
    ) {

   }

  ngOnInit() {
    this.activatedRoute.params.subscribe( x => this.profileId = x.id);
    this.profile = this.profileDataService.findProfileInList(this.profileId);
    if(!this.profile) {
      let severity = "error";
      let message = "Profile can't be loaded"
      this.toastService.toastMessage(severity, message, "There was an error retrieving the Profile Data");
      let newLog : LogVM = {
        message: message,
        severity: severity,
        profileId: this.profileId
      }
      this.logService.AddLog(newLog).subscribe();
      this.router.navigate(['/patients'])
    }
    this.isAdmin = this.userDataService.getIsAdmin();
  }
  deleteProfile() : void {
    this.profileService.deleteProfile(this.profile.id).subscribe( () => this.router.navigate(["/home"]));
  }

}
