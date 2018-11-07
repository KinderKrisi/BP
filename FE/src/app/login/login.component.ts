import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { AppComponent } from '../app.component';
import { LoginVM } from '../_models/_viewModels/loginVM';
import { AuthService } from '../_services/_auth/auth.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 
 /* loginForm: FormGroup;
  submitted = false;
  password: string;
  email: string;*/

  loginVM: LoginVM = new LoginVM();
  loading: boolean = false;

  constructor(private authService: AuthService, 
    private router: Router,
    private appComponent : AppComponent 
  ) { }

  ngOnInit() {
    this.loginVM.username = sessionStorage.getItem("username");
    this.loginVM.password = sessionStorage.getItem("password");
    if (this.loginVM.username && this.loginVM.password) {
      this.login();
    }
  }
  login() {
    this.loading = true;
    console.log("request send", this.loginVM)
    this.authService.login(this.loginVM).subscribe(x => {
      this.loading = false;
      if (x.length > 0) {
        sessionStorage.setItem("username", this.loginVM.username);
        sessionStorage.setItem("password", this.loginVM.password);
        this.authService.setUserProps(this.loginVM.username, this.loginVM.password);
        if (this.authService.redirectUrl && this.authService.redirectUrl.length > 0) {
          this.router.navigate([this.authService.redirectUrl]);
        }
        else {
          this.router.navigate(["/home"]);
        }
      }
    }, (err) => { this.loading = false; },
      () => { this.loading = false });
  }
}
