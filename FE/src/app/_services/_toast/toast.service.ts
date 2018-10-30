import { Injectable } from '@angular/core';
import {MessageService} from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private messageService: MessageService) { }

  registrationFailed(){
    this.messageService.add({severity:'error', summary:'Registration', detail:'Registration failed'});
  }
}
