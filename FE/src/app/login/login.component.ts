import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { UserService } from '../_services/_user/user.service';
import { AuthService } from '../_services/_auth/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  password: string;
  email: string;

  constructor(
  private formBuilder: FormBuilder,
  private authService: AuthService,
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.password = this.loginForm.get('password').value;
    this.email = this.loginForm.get("email").value;
    console.log("login request", this.email, this.password);
    this.login(this.email, this.password);
  }
  login(email: string, password : string): void{
    console.log("login in prgress");
    this.authService.login(email , password).subscribe();
  }
}
