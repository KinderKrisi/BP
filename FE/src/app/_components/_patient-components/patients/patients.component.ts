import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Patient } from 'src/app/_models/patient';
import { PatientService } from 'src/app/_services/patient/patient.service';
import { DataService } from 'src/app/_services/data/data.service';
import { ToastService } from 'src/app/_services/toast/toast.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {

  patientList : Patient[];
  adminPatientList : Patient[];

  isAdmin: boolean = false;

  constructor(private router: Router, private patientService: PatientService, private dataService: DataService, private toastService: ToastService) { }

  ngOnInit() {
    this.isAdmin = this.dataService.userGetIsAdmin();
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

  navigateToDetail(id: number) : void {
    this.router.navigate(['/patient', id]);
    console.log("navigated to detail")
  }

}
