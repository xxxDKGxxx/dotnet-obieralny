import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { ApplicationWithProviderTypeDto } from '../../services/applications/applications-model';
import { MatTableModule } from '@angular/material/table';
import { OfferDto } from '../../services/offers/offer-model';
import { OffersService } from '../../services/offers/offers-service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { forkJoin, map } from 'rxjs';

interface ApplicationWithOffer {
  application: ApplicationWithProviderTypeDto;
  offer: OfferDto;
}

@Component({
  selector: 'app-applications-list',
  imports: [MatTableModule],
  templateUrl: './applications-list.html',
  styleUrl: './applications-list.css',
})
export class ApplicationsList implements OnInit {
  @Input({ required: true })
  applications!: ApplicationWithProviderTypeDto[];

  protected applicationsWithOffers: ApplicationWithOffer[] = [];

  protected readonly columnsToDisplay = [
    'id',
    'offerTitle',
    'amount',
    'duration',
    'interestRate',
    'firstname',
    'lastname',
    'email',
    'status',
  ];

  private readonly offersService = inject(OffersService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    const requests = this.applications.map((app) =>
      this.offersService
        .getById(app.offerId, app.providerType)
        .pipe(map((offer) => ({ application: app, offer }))),
    );

    forkJoin(requests)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => {
        this.applicationsWithOffers = data;
      });
  }
  protected redirectToAppDetails(id: number) {
    void id;
    // console.log('In the future this will redirect to application id', id);
  }
}
