import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map, catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { UserVM } from '../../_models/_viewModels/userVM';
import { ToastService } from '../_toast/toast.service';

@Injectable()
export class UserService {

  private userUrl = '/api/user';
  
  constructor(private http: HttpClient, private toastService: ToastService) { }

  register(user : UserVM) : Observable<User> {
    return this.http.post<User>(this.userUrl, user).pipe(
      tap(returnedUser => console.log(returnedUser))
    )
  }
  login(email: string, password: string) : Observable<User> {
    return this.http.get<User>(this.userUrl)
  }
}