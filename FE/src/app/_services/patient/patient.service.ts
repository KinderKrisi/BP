import { Injectable } from '@angular/core';
import { DataService } from '../data/data.service';
import { Observable } from 'rxjs';
import { Patient } from 'src/app/_models/patient';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { PatientVM } from 'src/app/_models/_viewModels/patientVM';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  patientUrl = environment.apiUrl  + "patient"

  constructor(private dataService: DataService, private http: HttpClient) { }

  getAllPatientsForUser() : Observable<Patient[]> {
    return this.http.get<Patient[]>(this.patientUrl + "/GetAllPatientsForUser", {withCredentials: true}).pipe(
      tap(result => this.dataService.patientSetUserPatientList(result))
    )
  }
  createPatient(patient: PatientVM) : Observable<Patient> {
    return this.http.post<Patient>(this.patientUrl + "/CreatePatient", patient, {withCredentials: true}).pipe(
      tap(newPatient => this.dataService.patientPushToUserPatientList(newPatient))
    )
    }

}
