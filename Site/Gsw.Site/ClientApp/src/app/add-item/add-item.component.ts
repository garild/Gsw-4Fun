import { AlertService } from './../services/alert.service';
import { AuthenticationService } from './../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { BacklogService } from 'app/services/backlog/backlog.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BacklogItem, BacklogConstats, User } from 'app/models';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styles: ['./style.css']
})
export class AddItemComponent implements OnInit {
  backlogForm: FormGroup;
  backlogItem: BacklogItem;

  constructor(
    private backlogService: BacklogService,
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private alertService: AlertService
    ) { }

  get Items() { return this.backlogForm.controls; }
  get BacklogTypes() { return BacklogConstats.BacklogItemType() }
  get BacklogPriorities() { return BacklogConstats.BacklogPriorities() }
  get BacklogStatus() { return BacklogConstats.BacklogStatus() }

  ngOnInit() {
    this.backlogForm = this.formBuilder.group({
      backlogTitle: ['', Validators.required],
      backlogStatus: ['', Validators.required],
      backlogResolution: ['', Validators.required],
      description: ['', Validators.required],
      backlogType: ['', Validators.required],
      backlogPriority: ['', Validators.required],
      assignedUser: ['', Validators.required],
      reporterUser: ['', Validators.required],
    })

    this.authService.currentUser.subscribe(p => {
      this.backlogForm.setValue({
        backlogTitle: '',
        backlogStatus: 'Registered',
        backlogResolution: 'New',
        description: '',
        backlogType: '',
        backlogPriority: '',
        assignedUser: '',
        reporterUser: p.login
      })
    })
  }

  CreateBacklog() {

    this.backlogItem = {
      title: this.Items.backlogTitle.value,
      type: this.Items.backlogType.value,
      status: this.Items.backlogStatus.value,
      description: this.Items.description.value,
      assigneeUser: this.Items.assignedUser.value,
      reporterUser: this.Items.reporterUser.value,
      createAt: new Date(),
      priority: this.Items.backlogPriority.value,
    }

    this.backlogService.Create(this.backlogItem).subscribe(respons => {

      this.alertService.success('Item was created successfuly')
    }, error => {
      this.alertService.error(error)
    })
  }
}
