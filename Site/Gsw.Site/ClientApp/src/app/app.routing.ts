import { AddItemComponent } from './add-item/add-item.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { TableListComponent } from './components/table-list/table-list.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { AuthGuard } from './common/auth.guard';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { HomeComponent } from './components/home';

const routes: Routes = [

  { path: 'dashboard', component: DashboardComponent },
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'add-item', component: AddItemComponent },
  { path: 'table-list', component: TableListComponent },
  { path: 'notifications', component: NotificationsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'access-denied', component: AccessDeniedComponent },
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '**', component: DashboardComponent }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
