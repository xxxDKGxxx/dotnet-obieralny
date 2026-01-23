import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { ApplicationWithProviderTypeDto } from '../../services/applications/applications-model';
import { MatTableModule } from '@angular/material/table';
import { OfferDto } from '../../services/offers/offer-model';
import { OffersService } from '../../services/offers/offers-service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

interface ApplicationWithOffer {
  application: ApplicationWithProviderTypeDto;
  offer: OfferDto;
}

@Component({
  selector: 'app-applications-list',
  imports: [MatTableModule],
  templateUrl: './applications-list.html',
})
export class ApplicationsList implements OnInit {
  @Input({ required: true })
  applications!: ApplicationWithProviderTypeDto[];

  protected readonly applicationsWithOffers: ApplicationWithOffer[] = [];

  private readonly offersService = inject(OffersService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    for (let application of this.applications) {
      this.offersService
        .getById(application.offerId, application.providerType)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (offer) => {
            this.applicationsWithOffers.push({
              application: application,
              offer: offer,
            });
          },
        });
    }
  }
}
