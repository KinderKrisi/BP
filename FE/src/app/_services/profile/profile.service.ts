import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ToastService } from '../toast/toast.service';
import { HospitalProfileVM } from 'src/app/_models/_viewModels/hospitalProfileVM';
import { Observable, throwError, of } from 'rxjs';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProfileDataService } from '../_data-services/profile-data/profile-data.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private profileUrl = environment.apiUrl + 'profile';

  constructor(
    private http: HttpClient, 
    private toastService: ToastService, 
    private profileDataService: ProfileDataService
    ) { }

  createProfile(createProfile : HospitalProfileVM) : Observable<HospitalProfile> {
    return this.http.post<HospitalProfile>(this.profileUrl + "/CreateProfile", createProfile).pipe(
      tap(newProfile => this.profileDataService.pushToUserProfileList(newProfile)),
      catchError(x => {
        this.toastService.toastMessage("Error", "Create profile", x.error.msg[0]);
        return throwError(x);
      })
    )
  }
  getProfilesForUser() : Observable<HospitalProfile[]>{
    return this.http.get<HospitalProfile[]>(this.profileUrl + "/GetProfilesForUser").pipe(
    tap(userProfileList => this.profileDataService.setUserProfileList(userProfileList)))
   
  }
  getAllProfilesAdmin() : Observable<HospitalProfile[]>{
    return this.http.get<HospitalProfile[]>(this.profileUrl + "/GetAllProfiles").pipe(
    tap(adminProfileList => this.profileDataService.adminSetProfileList(adminProfileList))
    )
  }

  deleteProfile(id: number) : Observable<void> {
    return this.http.delete<void>(this.profileUrl + `/DeleteProfile/${id}`).pipe(
      tap(() => this.profileDataService.deleteProfileFromList(id)),
      catchError(x => {
        this.toastService.toastMessage("Error", "Delete profile", x.error.msg[0]);
        return throwError(x);
      })
    )
  }
  deleteProfileAdmin(id: number) : Observable<void> {
    return this.http.delete<void>(this.profileUrl + `/DeleteProfileAdmin/${id}`).pipe(
      catchError(x => {
        this.toastService.toastMessage("Error", "Delete profile admin", x.error.msg[0]);
        return throwError(x);
      })
    )
  }
}
