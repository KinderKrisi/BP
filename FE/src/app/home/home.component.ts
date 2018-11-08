import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../_services/profile/profile.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.profileService.dummyCall().subscribe();
  }

}
