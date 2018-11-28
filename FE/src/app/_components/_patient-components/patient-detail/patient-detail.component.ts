import { Component, OnInit } from '@angular/core';
import { Patient } from 'src/app/_models/patient';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from 'src/app/_services/patient/patient.service';
import { PatientDataService } from 'src/app/_services/_data-services/patient-data/patient-data.service';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';
import { ToastService } from 'src/app/_services/toast/toast.service';
import { LogVM } from 'src/app/_models/_viewModels/logVM';
import { LogsendService } from 'src/app/_services/logSend/logsend.service';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss']
})
export class PatientDetailComponent implements OnInit {

  patientId : number;
  patient : Patient;
  isEditingEnabled: boolean = false;
  isAdmin : boolean = false;

  constructor( 
    private activatedRoute : ActivatedRoute, 
    private patientService: PatientService, 
    private router: Router,
    private patientDataService: PatientDataService,
    private userDataService: UserDataService,
    private toastService : ToastService,
    private logService : LogsendService) {
   }

  ngOnInit() {
    this.activatedRoute.params.subscribe( x => this.patientId = x.id);
    this.patient = this.patientDataService.findPatientInList(this.patientId);
    if(!this.patient) {
      let severity = "error";
      let message = "Patient can't be loaded"
      this.toastService.toastMessage(severity, message, "There was an error retrieving the Patient Data");
      let newLog : LogVM = {
        message: message,
        severity: severity,
        patientId: this.patientId
      }
      this.logService.AddLog(newLog).subscribe();
      this.router.navigate(['/patients'])
    }
    this.isAdmin = this.userDataService.getIsAdmin();
    console.log(this.patient)
  }
  deletePatient() {
    this.patientService.deletePatient(this.patient.id).subscribe(
      () => {
        this.router.navigate(['/home']);
      } 
    );
  }

}
