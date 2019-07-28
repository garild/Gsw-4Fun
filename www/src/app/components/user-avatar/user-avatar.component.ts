import { Router } from '@angular/router';
import { AuthenticationService } from 'app/services';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/models';

@Component({
  selector: 'app-user-avatar',
  templateUrl: './user-avatar.component.html',
  styleUrls: ['./user-avatar.component.css']
})
export class UserAvatarComponent implements OnInit {

  public loggedUser: User;
  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.authService.currentUser.subscribe(x =>
      this.loggedUser = x
    );
  }

  LogOut() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
