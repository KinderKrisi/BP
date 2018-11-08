import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Profile } from 'selenium-webdriver/firefox';
import { HospitalProfile } from '../_models/hospitalProfile';
import { HospitalProfileVM } from '../_models/_viewModels/hospitalProfileVM';
import { ProfileService } from '../_services/profile/profile.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.css']
})
export class CreateProfileComponent implements OnInit {

  createProfileForm : FormGroup;
  submitted : boolean = false;
  user: User;

  constructor(private formBuilder: FormBuilder, private profileService: ProfileService) { }

  ngOnInit() {
    this.createProfileForm = this.formBuilder.group({
      nameOfHospital: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      address: ['', [Validators.required, Validators.minLength(6)], Validators.maxLength(20)],
      rate: [0, [Validators.required]]
    });
    this.user = JSON.parse(localStorage.get("currentUser"));
  }
  // convenience getter for easy access to form fields
  get f() { return this.createProfileForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createProfileForm.invalid) {
      return;
    }
     let profile : HospitalProfileVM = {
      nameOfHospital : this.createProfileForm.get('nameOfHospital').value,
      address: this.createProfileForm.get('address').value,
      rate: this.createProfileForm.get('rate').value,
      userId : this.user.id
      }
    console.log('Profile created ', profile);
    this.createProfile(profile);
  }

  private createProfile(profile: HospitalProfileVM) : void {
    this.profileService.createProfile(profile).subscribe()
  }


}
