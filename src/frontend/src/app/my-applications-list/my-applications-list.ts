import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ApplicationWithProviderTypeDto } from '../services/applications/applications-model';
import { AuthService } from '../services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { ApplicationsService } from '../services/applications/applications-service';
import { ApplicationsList } from '../common/applications-list/applications-list';

@Component({
  selector: 'app-my-applications-list',
  imports: [ApplicationsList],
  templateUrl: './my-applications-list.html',
})
export class MyApplicationsList implements OnInit {
  protected applications!: ApplicationWithProviderTypeDto[];

  private readonly authService = inject(AuthService);
  private readonly applicationsService = inject(ApplicationsService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.authService
      .getUserProfile()
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap((val) => {
          return this.applicationsService.listApplications(val.id, null);
        }),
      )
      .subscribe((apps) => {
        this.applications = apps;
      });
  }
}
