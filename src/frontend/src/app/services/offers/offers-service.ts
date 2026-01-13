import { inject, Injectable } from '@angular/core';
import { CalculatedOfferDto, OfferDto } from './offer-model';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { apiEndpoints } from '../../api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class OffersService {
  private readonly http = inject(HttpClient);

  public listOffers(amount: number, duration: number): Observable<OfferDto[]> {
    return this.http.get<OfferDto[]>(apiEndpoints.listOffers(amount, duration)).pipe(
      map((o) =>
        o.map((o) => ({
          ...o,
          validFrom: new Date(o.validFrom),
          validTo: new Date(o.validTo),
        })),
      ),
    );

    // return of([
    //   {
    //     id: 0,
    //     title: 'Testowy Title',
    //     description: 'Testowy desciption',
    //     minAmount: 1,
    //     maxAmount: 2,
    //     minDuration: 1,
    //     maxDuration: 2,
    //     minInterestRate: 0.0,
    //     maxInterestRate: 1.0,
    //     validFrom: new Date(Date.now()),
    //     validTo: new Date(Date.now()),
    //     providerType: ApplicationProviderType.ArdalisBank,
    //   },
    // ]);
  }

  public listCalculatedOffers(
    amount: number,
    duration: number,
    monthlyIncome: number,
    monthlyCosts: number,
    age: number,
    dependants: number,
  ): Observable<CalculatedOfferDto[]> {
    return this.http
      .get<
        CalculatedOfferDto[]
      >(apiEndpoints.listCalculatedOffers(amount, duration, monthlyIncome, monthlyCosts, age, dependants))
      .pipe(
        map((o) =>
          o.map((o) => ({
            ...o,
            validFrom: new Date(o.validFrom),
            validTo: new Date(o.validTo),
          })),
        ),
      );
    // return of([
    //   {
    //     id: 0,
    //     title: 'Testowy Title',
    //     description: 'Testowy Description',
    //     amount: 12000,
    //     duration: 12,
    //     interestRate: 4.5,
    //     validFrom: new Date(Date.now()),
    //     validTo: new Date(Date.now()),
    //     providerType: ApplicationProviderType.ArdalisBank,
    //   },
    // ]);
  }
}
