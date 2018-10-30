import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map, catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { ToastService } from '../_toast/toast.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authURL = "/api/auth"

  constructor(private http:HttpClient, private toastService:ToastService) { }

  login(email:string, password:string) : Observable<User>{
    return this.http.get<User>(this.authURL, {
      params: {
        email: `${email}`,
        password: `${password}`
      }
    }).pipe(
      tap(returnedUser => localStorage.setItem("currentUser", JSON.stringify(returnedUser)))
    )
  }
}
