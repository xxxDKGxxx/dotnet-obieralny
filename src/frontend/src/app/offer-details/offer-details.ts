import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { combineLatest } from 'rxjs';
import { OffersService } from '../services/offers/offers-service';
import { CalculatedOfferDto, OfferDto } from '../services/offers/offer-model';
import { CalculatedOffer } from '../common/calculated-offer/calculated-offer';
import { Offer } from '../common/offer/offer';
import { ApplicationForm, OfferConditions } from '../common/application-form/application-form';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApplicationProviderType } from '../shared/enum';

@Component({
  selector: 'app-offer-details',
  imports: [CalculatedOffer, Offer, ApplicationForm],
  templateUrl: './offer-details.html',
})
export class OfferDetails implements OnInit {
  protected offerId!: number;
  protected amount!: number | null;
  protected duration!: number | null;
  protected monthlyIncome!: number | null;
  protected monthlyCosts!: number | null;
  protected age!: number | null;
  protected dependants!: number | null;
  protected providerType!: ApplicationProviderType;
  protected offerDto!: OfferDto | null;
  protected calculatedOfferDto!: CalculatedOfferDto | null;

  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly offersService = inject(OffersService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    combineLatest([this.activatedRoute.paramMap, this.activatedRoute.queryParamMap])
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(([params, queryParams]) => {
        const offerIdStr = params.get('offerId');
        const providerTypeStr = queryParams.get('providerType');

        if (!offerIdStr || !providerTypeStr) {
          return;
        }

        this.offerId = Number.parseInt(offerIdStr, 10);
        this.amount = Number.parseInt(queryParams.get('amount') ?? '0', 10);
        this.duration = Number.parseInt(queryParams.get('duration') ?? '0', 10);
        this.monthlyIncome = Number.parseInt(queryParams.get('monthlyIncome') ?? '0', 10);
        this.monthlyCosts = Number.parseInt(queryParams.get('monthlyCosts') ?? '0', 10);
        this.age = Number.parseInt(queryParams.get('age') ?? '0', 10);
        this.dependants = Number.parseInt(queryParams.get('dependants') ?? '0', 10);
        this.providerType = providerTypeStr as ApplicationProviderType;

        this.fetchOffer();
      });
  }

  protected updateOfferBasedOnNewConditions($event: OfferConditions) {
    this.amount = $event.amount;
    this.duration = $event.duration;
    this.monthlyIncome = $event.monthlyIncome;
    this.monthlyCosts = $event.monthlyCosts;
    this.age = $event.age;
    this.dependants = $event.dependants;

    this.fetchOffer();
  }

  protected additionalInfoProvided(): boolean {
    return (
      !!this.amount &&
      !!this.duration &&
      !!this.monthlyIncome &&
      !!this.monthlyCosts &&
      !!this.age &&
      !!this.dependants
    );
  }

  protected fetchOffer() {
    if (!this.offerId || !this.providerType) {
      return;
    }

    if (this.additionalInfoProvided()) {
      this.offersService
        .getCalculatedById(
          this.offerId,
          this.amount!,
          this.duration!,
          this.monthlyIncome!,
          this.monthlyCosts!,
          this.age!,
          this.dependants!,
          this.providerType!,
        )
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (offer) => {
            this.calculatedOfferDto = offer;
          },
        });
    } else {
      this.offersService
        .getById(this.offerId, this.providerType)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (offer) => {
            this.calculatedOfferDto = null;
            this.offerDto = offer;
          },
        });
    }
  }
}
