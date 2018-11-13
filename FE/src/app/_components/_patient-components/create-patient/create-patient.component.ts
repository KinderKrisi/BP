import { Component, OnInit } from '@angular/core';
import { PatientVM } from 'src/app/_models/_viewModels/patientVM';
import { PatientService } from 'src/app/_services/patient/patient.service';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css']
})
export class CreatePatientComponent implements OnInit {

  newPatient : PatientVM;
  submitted : boolean = false;

  constructor(private patientService: PatientService) { }

  ngOnInit() {
    this.newPatient = new PatientVM();
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
    this.patientService.createPatient(newPatient).subscribe();
  }

}
