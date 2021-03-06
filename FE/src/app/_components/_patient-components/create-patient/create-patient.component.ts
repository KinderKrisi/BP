import { Component, OnInit } from '@angular/core';
import { PatientVM } from 'src/app/_models/_viewModels/patientVM';
import { PatientService } from 'src/app/_services/patient/patient.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.scss']
})
export class CreatePatientComponent implements OnInit {

  newPatient : PatientVM;
  submitted : boolean = false;

  constructor(private patientService: PatientService, private router: Router) { }

  ngOnInit() {
    this.newPatient = new PatientVM();
    console.log(this.newPatient);
  }

  onSubmit() {
    this.submitted = true;

    if(this.newPatient){
      this.createPatient(this.newPatient);
    }
    else{
      return;
    }
  }
    
  private createPatient(newPatient: PatientVM) : void {
    console.log(" new Patient",newPatient);
    this.patientService.createPatient(newPatient).subscribe(() => this.router.navigate(["/home"]));
  }
  showDate(date : Date) {
    console.log(date);
  }
}
