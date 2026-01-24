import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ApplicationsService } from '../services/applications/applications-service';
import { ApplicationProviderType } from '../services/offers/offer-model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApplicationWithProviderTypeDto } from '../services/applications/applications-model';
import { ApplicationsList } from '../common/applications-list/applications-list';

@Component({
  selector: 'app-all-applications-list',
  imports: [ApplicationsList],
  templateUrl: './all-applications-list.html',
})
export class AllApplicationsList implements OnInit {
  protected applications!: ApplicationWithProviderTypeDto[];

  private readonly applicationsService = inject(ApplicationsService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.applicationsService
      .listApplications(null, ApplicationProviderType.ArdalisBank)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((apps) => {
        this.applications = apps;
      });
  }
}
