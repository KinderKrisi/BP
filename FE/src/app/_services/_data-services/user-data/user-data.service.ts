import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  private isAdmin : boolean = false;

  constructor() { }

  setIsAdmin(isAdmin : boolean) : void{
    this.isAdmin = isAdmin;
  }
  getIsAdmin() : boolean{
    return this.isAdmin;
  }
  
}
