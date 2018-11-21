import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/_services/_data-services/user-data/user-data.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {


  isAdmin : boolean = false;



  constructor(private userDataService: UserDataService) { }

  ngOnInit() {
    this.isAdmin = this.userDataService.getIsAdmin();
  }
}
