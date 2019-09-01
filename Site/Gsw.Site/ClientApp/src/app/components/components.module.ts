import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { UserAvatarComponent } from './user-avatar/user-avatar.component';
import { MatButtonModule, MatRippleModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatTooltipModule } from '@angular/material';
import { HomeComponent } from './home/home.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    MatRippleModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatTooltipModule,
  ],
  declarations: [
    NavbarComponent,
    SidebarComponent,
    UserAvatarComponent,
    HomeComponent,
  ],
  exports: [
    NavbarComponent,
    SidebarComponent
  ]
})
export class ComponentsModule { }
