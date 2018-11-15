import { Component, OnInit } from '@angular/core';
import { Patient } from 'src/app/_models/patient';
import { ActivatedRoute } from '@angular/router';
import { DataService } from 'src/app/_services/data/data.service';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss']
})
export class PatientDetailComponent implements OnInit {

  patientId : number;
  patient : Patient;

  constructor( private activatedRoute : ActivatedRoute, private dataService: DataService) {
    this.activatedRoute.params.subscribe( x => this.patientId = x.id);
    this.patient = this.dataService.patientFindPatientInList(this.patientId);
   }

  ngOnInit() {
    console.log(this.patient)
  }

}
