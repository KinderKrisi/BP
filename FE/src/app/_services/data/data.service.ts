import { Injectable } from '@angular/core';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { Observable } from 'rxjs';
import { Patient } from 'src/app/_models/patient';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  userProfileList : HospitalProfile[] = [];
  userPatientList : Patient[] = [];
  constructor() { }

  //Profiles
  profileSetUserProfileList(list: HospitalProfile[]) : void {
    console.log("returned list", list)
    this.userProfileList = list;
    console.log("final", this.userProfileList)
  }
  profileGetUserProfileList() : HospitalProfile[] {
    return this.userProfileList;
  }
  profilePushToUserProfileList(profile: HospitalProfile) : void {
    if(profile){
    this.userProfileList.push(profile)
    }
  }
  profileFindProfileInList(id: number) : HospitalProfile {
   return this.userProfileList.find(x => x.id == id)
  }
  profileDeleteProfileFromList(id: number) : HospitalProfile[] {
    this.userProfileList = this.userProfileList.filter(x => x.id != id);
    return this.userProfileList;
  }



  // Patient
  patientSetUserPatientList(list: Patient[]) : void {
    this.userPatientList = list;
  }
  patientGetUserPatientList() : Patient[] {
    return this.userPatientList;
  }
  patientPushToUserPatientList(patient: Patient) : void {
    this.userPatientList.push(patient)
  }
  patientFindPatientInList(id: number) : Patient {
    return this.userPatientList.find(x => x.id == id)
  }
}
