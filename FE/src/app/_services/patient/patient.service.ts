import { Injectable } from '@angular/core';
import { DataService } from '../data/data.service';
import { Observable, throwError } from 'rxjs';
import { Patient } from 'src/app/_models/patient';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { PatientVM } from 'src/app/_models/_viewModels/patientVM';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  patientUrl = environment.apiUrl  + "patient"

  constructor(private dataService: DataService, private http: HttpClient, private toastService: ToastService) { }

  getAllPatientsForUser() : Observable<Patient[]> {
    return this.http.get<Patient[]>(this.patientUrl + "/GetAllPatientsForUser").pipe(
      tap(result => 
        {
          this.dataService.patientSetUserPatientList(result);
        }),
        catchError(err => {
          this.toastService.toastMessage("error", "Get patients", err.error.msg[0]);
          return throwError(err)
        })
    )
  }
  getAllUserPatientsAdmin() : Observable<Patient[]> {
    return this.http.get<Patient[]>(this.patientUrl + '/GetAllPatientsAdmin').pipe(
        tap(adminPatientsList => this.dataService.patientAdminSetPatientList(adminPatientsList))
        ,catchError(err => {
          this.toastService.toastMessage("error", "Get patients admin", err.error.msg[0]);
          return throwError(err)
        })
    )
  }

  createPatient(patient: PatientVM) : Observable<Patient> {
    return this.http.post<Patient>(this.patientUrl + "/CreatePatient", patient).pipe(
      tap(newPatient => {
        this.dataService.patientPushToUserPatientList(newPatient)
      }),
        catchError(err => {
          this.toastService.toastMessage("error", "Create patient", err.error.msg[0]);
          return throwError(err)
        })
    )
    }

}
