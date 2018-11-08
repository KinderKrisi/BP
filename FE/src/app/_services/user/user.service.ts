import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map, catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { UserVM } from '../../_models/_viewModels/userVM';
import { ToastService } from '../toast/toast.service';
import { ChangePasswordVM } from 'src/app/_models/_viewModels/changePasswordVM';

@Injectable()
export class UserService {

  private userUrl = '/api/user';
  
  constructor(private http: HttpClient, private toastService: ToastService) { }

  register(user : UserVM) : Observable<User> {
    return this.http.post<User>(this.userUrl + "/CreateUser", user).pipe(
      tap(returnedUser => console.log(returnedUser))
    )
  }
  
  logout() : void{
    localStorage.removeItem("currentUser")
  }

  updateDetails(changePassword: ChangePasswordVM) : Observable<User>{
    return this.http.put<User>(this.userUrl + "/ChangePassword", changePassword).pipe(
      tap(returnedUser => localStorage.setItem("currentUser", JSON.stringify(returnedUser)))
    )
  }
}