import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HospitalProfileVM } from 'src/app/_models/_viewModels/hospitalProfileVM';
import { ProfileService } from 'src/app/_services/profile/profile.service';
import { OpenIdConnectService } from 'src/app/_services/openIdConnect/open-id-connect.service';


@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.css']
})
export class CreateProfileComponent implements OnInit {

  submitted : boolean = false;
  newProfile: HospitalProfileVM;

  constructor(private formBuilder: FormBuilder, private profileService: ProfileService, private openidConnectService:  OpenIdConnectService) { }

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
    this.profileService.createProfile(profile).subscribe()
  }


}
