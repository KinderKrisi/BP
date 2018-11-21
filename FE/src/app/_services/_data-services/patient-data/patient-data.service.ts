import { Injectable } from '@angular/core';
import { Patient } from 'src/app/_models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientDataService {

  private userPatientList : Patient[] = [];
  private adminPatientList: Patient[] = [];

  constructor() { }


  setUserPatientList(list: Patient[]) : void {
    this.userPatientList = list;
  }
  getUserPatientList() : Patient[] {
    return this.userPatientList;
  }
  pushToUserPatientList(patient: Patient) : void {
    this.userPatientList.push(patient)
  }
  findPatientInList(id: number) : Patient {
    return this.userPatientList.find(x => x.id == id)
  }
  adminSetPatientList(list: Patient[]) : void {
    this.adminPatientList = list;
  }
  adminGetPatientList() : Patient[] {
    return this.adminPatientList;
  }
  deletePatientFromList(patientId: number) {
    this.userPatientList = this.userPatientList.filter(x => x.id !== patientId)
  }
}
