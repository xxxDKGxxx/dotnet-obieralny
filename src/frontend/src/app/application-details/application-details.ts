import { Component, inject, OnInit } from '@angular/core';
import { ApplicationWithProviderTypeDto } from '../services/applications/applications-model';
import { ApplicationsService } from '../services/applications/applications-service';

@Component({
  selector: 'app-application-details',
  imports: [],
  templateUrl: './application-details.html',
})
export class ApplicationDetails implements OnInit {
  protected application!: ApplicationWithProviderTypeDto;

  private readonly applicationsService = inject(ApplicationsService);

  ngOnInit(): void {}
}
