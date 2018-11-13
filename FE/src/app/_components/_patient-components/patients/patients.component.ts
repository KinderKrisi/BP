import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Patient } from 'src/app/_models/patient';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PatientService } from 'src/app/_services/patient/patient.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {

  patientList : Patient[];
  private unsubscribe = new Subject<void>();

  constructor(private router: Router, private patientService: PatientService) { }

  ngOnInit() {
  }

  getUsersPatients() : void {
    this.patientService.getAllPatientsForUser().subscribe(x => this.patientList = x)
  }

  navigateToDetail(id: number) : void {
    this.router.navigate(['/patient', id]);
    console.log("navigated to detail")
  }

}
