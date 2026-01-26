import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ApplicationWithProviderTypeDto } from '../services/applications/applications-model';
import { ApplicationsService } from '../services/applications/applications-service';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { combineLatest, EMPTY, switchMap } from 'rxjs';
import { CalculatedOfferDto } from '../services/offers/offer-model';
import { ApplicationForm } from '../common/application-form/application-form';
import { OffersService } from '../services/offers/offers-service';
import { CalculatedOffer } from '../common/calculated-offer/calculated-offer';
import { ApplicationManagementPanel } from './application-management-panel/application-management-panel';

@Component({
  selector: 'app-application-details',
  imports: [ApplicationForm, CalculatedOffer, ApplicationManagementPanel],
  templateUrl: './application-details.html',
})
export class ApplicationDetails implements OnInit {
  protected application!: ApplicationWithProviderTypeDto;
  protected calculatedOffer!: CalculatedOfferDto;

  private readonly applicationsService = inject(ApplicationsService);
  private readonly offersService = inject(OffersService);
  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);

  ngOnInit(): void {
    combineLatest([this.activatedRoute.params, this.activatedRoute.queryParams])
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap(([params, queryParams]) => {
          const applicationId = params['applicationId'];
          const providerType = queryParams['providerType'];

          if (!applicationId || !providerType) {
            this.router.navigateByUrl('/');
            return EMPTY;
          }

          const applicationIdAsNumber = Number.parseInt(applicationId);

          if (isNaN(applicationIdAsNumber)) {
            this.router.navigateByUrl('/');
            return EMPTY;
          }

          return this.applicationsService.getById(applicationIdAsNumber, providerType);
        }),
        switchMap((app) => {
          this.application = app;

          return this.offersService.getById(
            this.application.offerId,
            this.application.providerType,
          );
        }),
      )
      .subscribe((offer) => {
        this.calculatedOffer = {
          id: offer.id,
          title: offer.title,
          description: offer.description,
          amount: this.application.offerConditions.amount,
          duration: this.application.offerConditions.duration,
          interestRate: this.application.offerConditions.interestRate,
          validFrom: offer.validFrom,
          validTo: offer.validTo,
          providerType: this.application.providerType,
        };
      });
  }
}
