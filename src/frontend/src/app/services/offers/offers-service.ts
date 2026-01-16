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

  public getById(id: number, providerType: string): Observable<OfferDto> {
    return this.http.get<OfferDto>(apiEndpoints.getOfferById(id, providerType)).pipe(
      map((o) => ({
        ...o,
        validFrom: new Date(o.validFrom),
        validTo: new Date(o.validTo),
      })),
    );
  }

  public getCalculatedById(
    id: number,
    amount: number,
    duration: number,
    monthlyIncome: number,
    monthlyCosts: number,
    age: number,
    dependants: number,
    providerType: string,
  ) {
    return this.http
      .get<CalculatedOfferDto>(
        apiEndpoints.getCalculatedOfferById(
          id,
          amount,
          duration,
          monthlyIncome,
          monthlyCosts,
          age,
          dependants,
          providerType,
        ),
      )
      .pipe(
        map((o) => ({
          ...o,
          validFrom: new Date(o.validFrom),
          validTo: new Date(o.validTo),
        })),
      );
  }

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
  }
}
