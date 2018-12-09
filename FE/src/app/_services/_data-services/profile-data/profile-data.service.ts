import { Injectable } from '@angular/core';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';

@Injectable({
  providedIn: 'root'
})
export class ProfileDataService {

  private userProfileList : HospitalProfile[] = [];
  private adminProfileList : HospitalProfile[]= []

  constructor() { }
  
    setUserProfileList(list: HospitalProfile[]) : void {
      this.userProfileList = list;
    }
    getUserProfileList() : HospitalProfile[] {
      return this.userProfileList;
    }
    pushToUserProfileList(profile: HospitalProfile) : void {
      if(profile){
      this.userProfileList.push(profile)
      }
    }
    findProfileInList(id: number) : HospitalProfile {
     return this.userProfileList.find(x => x.id == id)
    }
    deleteProfileFromList(id: number) : HospitalProfile[] {
      this.userProfileList = this.userProfileList.filter(x => x.id != id);
      return this.userProfileList;
    }
    adminDeleteProfileFromList(id: number) : HospitalProfile[] {
      this.adminProfileList = this.adminProfileList.filter(x => x.id != id);
      return this.adminProfileList;
    }
    adminSetProfileList(list: HospitalProfile[]): void {
      this.adminProfileList = list;
    }
    adminGetProfileList() : HospitalProfile[] {
      return this.adminProfileList;
    }
    updateProfile(updatedProfile: HospitalProfile) : void {
      let dummy1 = this.userProfileList.find(x => x.id == updatedProfile.id);
      if(dummy1) dummy1 = updatedProfile;
      let dummy2 = this.adminProfileList.find(x => x.id == updatedProfile.id);
      if(dummy2) dummy2 = updatedProfile;
    }
  
}
