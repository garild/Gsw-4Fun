import { AuthenticationService } from './../../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/models';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  loggedUser: User;

  constructor(private authServices: AuthenticationService) {
     this.authServices.currentUser.subscribe(p => {
        this.loggedUser = p;
      })
   }

   ngOnInit() {
  }

}
