import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LogVM } from 'src/app/_models/_viewModels/logVM';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogsendService {

  logUrl = environment.apiUrl + "log";

  constructor(private http : HttpClient ) { }

  AddLog(newLog: LogVM) : Observable<LogVM> {
    return this.http.post<LogVM>(this.logUrl + "/AddLogFE", newLog)
  }
}
