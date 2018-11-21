import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Patient } from 'src/app/_models/patient';
import { PatientService } from 'src/app/_services/patient/patient.service';
import { ToastService } from 'src/app/_services/toast/toast.service';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {

  patientList : Patient[];
  adminPatientList : Patient[];

  isAdmin: boolean = false;

  constructor(
    private router: Router,
    private patientService: PatientService,
    private toastService: ToastService,
    private userDataService: UserDataService  
      ) { }

  ngOnInit() {
    this.isAdmin = this.userDataService.getIsAdmin();
    this.patientService.getAllPatientsForUser().subscribe(x => this.patientList = x)
    if(this.isAdmin) this.patientService.getAllUserPatientsAdmin().subscribe(x => this.adminPatientList = x);
  }
  getUsersPatients() : void {
    this.patientService.getAllPatientsForUser().subscribe(x => {
      this.patientList = x;
      this.toastService.toastMessage("success", "Patients Refresh", "Users patients had been refreshed");
    })
  }

  getAdminPatients() : void {
    if(this.isAdmin) this.patientService.getAllUserPatientsAdmin().subscribe(x =>{
       this.adminPatientList = x;
       this.toastService.toastMessage("success", "Admin Patients refresh", "Admin patients had been refreshed")
    })
  }

  deletePatient(patientId: number){
    this.patientService.deletePatient(patientId).subscribe();
  }
  deletePatientAdmin(patientId: number){
    if(this.isAdmin) this.patientService.deletePatientAdmin(patientId).subscribe();
  }

  navigateToDetail(id: number) : void {
    this.router.navigate(['/patient', id]);
    console.log("navigated to detail")
  }

}
