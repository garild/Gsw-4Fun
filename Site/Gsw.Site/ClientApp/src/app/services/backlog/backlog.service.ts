import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { BacklogItem } from 'app/models/Backlog/backlog';
import { environment } from 'environments/environment';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class BacklogService {

  constructor(private httpclient: HttpClient) { }

  Create(backlogItem: BacklogItem) {
    return this.httpclient.post(`${environment.baseApi}/api/backlog/create`, JSON.stringify(backlogItem));
  }
}
