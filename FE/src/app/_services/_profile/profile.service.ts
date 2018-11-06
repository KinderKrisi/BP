import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastService } from '../_toast/toast.service';
import { HospitalProfileVM } from 'src/app/_models/_viewModels/hospitalProfileVM';
import { Observable } from 'rxjs';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private profileUrl = '/api/profile';
  constructor(private http:HttpClient, private toastService: ToastService) { }

  createProfile(createProfile : HospitalProfileVM) : Observable<HospitalProfile> {
    return this.http.post<HospitalProfile>(this.profileUrl + "/CreateProfile", createProfile).pipe(
      tap(returnedUser => console.log(returnedUser))
    )
  }
}
