import { Component, OnInit } from '@angular/core';
import { HospitalProfileVM } from 'src/app/_models/_viewModels/hospitalProfileVM';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.scss']
})
export class CreateProfileComponent implements OnInit {

  submitted : boolean = false;
  newProfile: HospitalProfileVM;

  constructor(private profileService: ProfileService, private router: Router) { }

  ngOnInit() {
    this.newProfile = new HospitalProfileVM();
  }

  onSubmit() {
    this.submitted = true;

    if(this.newProfile){
    this.createProfile(this.newProfile);
    }
    else{
      return;
    }
  }

  private createProfile(profile: HospitalProfileVM) : void {
    this.profileService.createProfile(profile).subscribe(() => this.router.navigate(['/home']));
  }


}
