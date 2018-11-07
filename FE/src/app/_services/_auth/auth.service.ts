
import { of as observableOf, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { LoginVM } from 'src/app/_models/_viewModels/loginVM';

@Injectable()
export class AuthService {

  jwtHelperService: JwtHelperService;
  userName: string;
  password: string;
  private refreshTokenCall: Observable<string>;
  // store the URL so we can redirect after logging in
  redirectUrl: string;

  constructor(private http: HttpClient, private router: Router) {
    this.jwtHelperService = new JwtHelperService();

    if (sessionStorage.getItem("user_name")) {
      this.userName = sessionStorage.getItem("user_name");
    }
  }

  setUserProps(userName: string, password: string) {
    this.userName = userName;
    this.password = password;
    sessionStorage.setItem("TfUserName", userName);
    sessionStorage.setItem("TfPassword", password);
  }


  saveTokenInLocalStorage(token) {
    sessionStorage.setItem('TfToken', token);
  }

  isAuthorized(): boolean {
    if (this.userName) {
      return true;
    } else {
      return false;
    }
  }

  getToken(): Observable<string> {
    if (this.userName) {
      let token = localStorage.getItem('TfToken');
      if (token) {
        let isTokenExpired = this.jwtHelperService.isTokenExpired(token);
        if (!isTokenExpired) {
          return observableOf(token);
        }
      }
      return this.refreshToken();
    } else {
      return observableOf(undefined);
    }
  }

  private refreshToken(): Observable<string> {
    if (!this.refreshTokenCall) {
      this.refreshTokenCall = this.http.post<string>(environment.apiUrl + "account/createToken", { userName: this.userName, password: this.password }, { withCredentials: true })
        .pipe(
          tap(this.saveTokenInLocalStorage),
          tap(() => this.refreshTokenCall = null)
        );

    }
    return this.refreshTokenCall;
  }

  public login(loginVm: LoginVM): Observable<string> {
    return this.http.post(environment.apiUrl + "account/createToken", loginVm, { withCredentials: true })
      .pipe(
        tap((response: string) => {
          sessionStorage.clear();
          this.saveTokenInLocalStorage(response);
        })
      );
  }
}
