import { Injectable } from '@angular/core';
import { map, catchError, finalize } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ToastService } from '../toast/toast.service';
import { HospitalProfileVM } from 'src/app/_models/_viewModels/hospitalProfileVM';
import { Observable, Subject } from 'rxjs';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DataService } from '../data/data.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private profileUrl = environment.apiUrl + 'profile';

  constructor(private http:HttpClient, private toastService: ToastService, private dataService: DataService) { }

  createProfile(createProfile : HospitalProfileVM) : Observable<HospitalProfile> {
    return this.http.post<HospitalProfile>(this.profileUrl + "/CreateProfile", createProfile, {withCredentials: true}).pipe(
      tap(newProfile => this.dataService.profilePushToUserProfileList(newProfile))
    )
  }
  getProfilesForUser() : Observable<HospitalProfile[]>{
    return this.http.get<HospitalProfile[]>(this.profileUrl + "/GetProfilesForUser", {withCredentials: true})
    .pipe(tap(userProfileList => this.dataService.profileSetUserProfileList(userProfileList)))
    
  }
  getAllProfiles() : Observable<HospitalProfile[]>{
    return this.http.get<HospitalProfile[]>(this.profileUrl + "/GetAllProfiles", {withCredentials: true})
  }
  deleteProfile(id: number) : Observable<void> {
    return this.http.delete<void>(this.profileUrl + `/DeleteProfile/${id}`, {withCredentials: true}).pipe(
      tap(() => this.dataService.profileDeleteProfileFromList(id))
    )
  }
}
