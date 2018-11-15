import { Injectable } from '@angular/core';
import { HospitalProfile } from 'src/app/_models/hospitalProfile';
import { Observable } from 'rxjs';
import { Patient } from 'src/app/_models/patient';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private userProfileList : HospitalProfile[] = [];
  private adminProfileList : HospitalProfile[]= []

  private userPatientList : Patient[] = [];
  private adminPatientList: Patient[] = [];

  private isAdmin : boolean = false;
  constructor() { }

  //User
  userSetIsAdmin(isAdmin : boolean) : void{
    this.isAdmin = isAdmin;
  }
  userGetIsAdmin() : boolean{
    return this.isAdmin;
  }

  

  //Profiles
  profileSetUserProfileList(list: HospitalProfile[]) : void {
    this.userProfileList = list;
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
  profileAdminSetProfileList(list: HospitalProfile[]): void {
    this.adminProfileList = list;
  }
  profileAdminGetProfileList() : HospitalProfile[] {
    return this.adminProfileList;
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
  patientAdminSetPatientList(list: Patient[]) : void {
    this.adminPatientList = list;
  }
  patientAdminGetPatientList() : Patient[] {
    return this.adminPatientList;
  }
}
