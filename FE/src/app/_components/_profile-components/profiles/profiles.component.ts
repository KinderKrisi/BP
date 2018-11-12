import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profiles',
  templateUrl: './profiles.component.html',
  styleUrls: ['./profiles.component.css']
})
export class ProfilesComponent implements OnInit {

  userProfileList : HospitalProfile[] = [];
  allProfileList : HospitalProfile[] = [];
  profiles : Observable<HospitalProfile[]>;

  constructor(private profileService: ProfileService, private router: Router) { }


  ngOnInit() {
    
  }

  getUsersProfiles() : void {
    this.profileService.getProfilesForUser().subscribe(x => this.userProfileList = x)
  }

  getAllProfiles() : void {
    this.profileService.getAllProfilesAdmin().subscribe(x => this.allProfileList = x)
  }
  navigateToDetail(id: number) : void {
    this.router.navigate(['/profile', id]);
    console.log("navigated to detail")
  }

}
