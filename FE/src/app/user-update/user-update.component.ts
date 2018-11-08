import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserVM } from '../_models/_viewModels/userVM';
import { UserService } from '../_services/user/user.service';
import { User } from '../_models/user';
import { ChangePasswordVM } from '../_models/_viewModels/changePasswordVM';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {

  userUpdateForm: FormGroup;
  submitted = false;
  user: User;

  constructor(private userService: UserService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.user = JSON.parse(localStorage.get("currentUser"));
    this.userUpdateForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }
  // convenience getter for easy access to form fields
  get f() { return this.userUpdateForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.userUpdateForm.invalid) {
      return;
    }
     let passwordChange : ChangePasswordVM = {
      oldPassword: this.userUpdateForm.get('oldPassword').value,
      newPassword: this.userUpdateForm.get('newPassword').value,
      userId: this.user.id
      }
    console.log('Password changed', passwordChange);
    this.changePassword(passwordChange);
  }

  private changePassword(changePassword : ChangePasswordVM) : void {
    this.userService.updateDetails(changePassword).subscribe();
  }
}
