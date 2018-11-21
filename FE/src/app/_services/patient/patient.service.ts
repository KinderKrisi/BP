import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Patient } from 'src/app/_models/patient';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { PatientVM } from 'src/app/_models/_viewModels/patientVM';
import { ToastService } from '../toast/toast.service';
import { PatientDataService } from '../_data-services/patient-data/patient-data.service';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  patientUrl = environment.apiUrl  + "patient"

  constructor(
    private http: HttpClient, 
    private toastService: ToastService, 
    private patientDataService: PatientDataService
    ) { }

  getAllPatientsForUser() : Observable<Patient[]> {
    return this.http.get<Patient[]>(this.patientUrl + "/GetAllPatientsForUser").pipe(
      tap(result => 
        {
          this.patientDataService.setUserPatientList(result);
        }),
        catchError(err => {
          this.toastService.toastMessage("error", "Get patients", err.error.msg[0]);
          return throwError(err)
        })
    )
  }
  getAllUserPatientsAdmin() : Observable<Patient[]> {
    return this.http.get<Patient[]>(this.patientUrl + '/GetAllPatientsAdmin').pipe(
        tap(adminPatientsList => this.patientDataService.adminSetPatientList(adminPatientsList))
        ,catchError(err => {
          this.toastService.toastMessage("error", "Get patients admin", err.error.msg[0]);
          return throwError(err)
        })
    )
  }

  createPatient(patient: PatientVM) : Observable<Patient> {
    return this.http.post<Patient>(this.patientUrl + "/CreatePatient", patient).pipe(
      tap(newPatient => {
        this.patientDataService.pushToUserPatientList(newPatient)
      }),
        catchError(err => {
          this.toastService.toastMessage("error", "Create patient", err.error.msg[0]);
          return throwError(err)
        })
    )
    }
    deletePatient(patientId: number) : Observable<any> {
      return this.http.delete<any>(this.patientUrl + `/DeletePatient/${patientId}`).pipe(
        tap(() => {
          this.patientDataService.deletePatientFromList(patientId);
        }),
        catchError(err => {
          this.toastService.toastMessage("error", "Delete patient", err.error.msg[0]);
          return throwError(err);
        })
      )
    }
    deletePatientAdmin(patientId: number) : Observable<any> {
      return this.http.delete<any>(this.patientUrl + `/DeletePatientAdmin/${patientId}`).pipe(
        tap(() => {
          this.patientDataService.deletePatientFromList(patientId);
        }),
        catchError(err => {
          this.toastService.toastMessage("error", "Delete patient", err.error.msg[0]);
          return throwError(err);
        })
      )
    }

}
