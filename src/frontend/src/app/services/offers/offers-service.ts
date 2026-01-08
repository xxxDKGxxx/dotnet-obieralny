import { Injectable } from '@angular/core';
import { ApplicationProviderType, OfferDto } from './offer-model';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OffersService {
  public listOffers(amount: number, duration: number): Observable<OfferDto[]> {
    (void amount, duration);

    return of([
      {
        id: 0,
        title: 'Testowy Title',
        description: 'Testowy desciption',
        minAmount: 1,
        maxAmount: 2,
        minDuration: 1,
        maxDuration: 2,
        minInterestRate: 0.0,
        maxInterestRate: 1.0,
        validFrom: new Date(Date.now()),
        validTo: new Date(Date.now()),
        providerType: ApplicationProviderType.ArdalisBank,
      },
    ]);
  }
}
