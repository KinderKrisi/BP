import { Injectable } from '@angular/core';
import {MessageService} from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private messageService: MessageService) { }

  //Message severities
  //success - green
  //info - blue
  //warn - orange
  //error - red

  toastMessage(severity: string, summary : string, detail: string){
    this.messageService.add({severity: severity, summary: summary, detail: detail})
  }
  clear() {
    this.messageService.clear();
  }
}
