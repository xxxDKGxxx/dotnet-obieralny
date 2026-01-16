import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { OffersList } from './offers-list/offers-list';
import { OffersService } from '../services/offers/offers-service';
import {
  ApplicationProviderType,
  CalculatedOfferDto,
  OfferDto,
} from '../services/offers/offer-model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApplicationRoutes } from '../app.routes';
import { AuthService } from '../services/auth.service';
import { debounceTime, map, of, Subject, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-full-search',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    FormsModule,
    OffersList,
  ],
  templateUrl: './full-search.html',
})
export class FullSearch implements OnInit {
  protected amount!: number;
  protected duration!: number;
  protected monthlyIncome!: number;
  protected monthlyCosts!: number;
  protected age!: number;
  protected dependants!: number;
  protected calculatedOffers!: CalculatedOfferDto[];
  protected offers!: OfferDto[];

  private readonly activatedRoute = inject(ActivatedRoute);
  private readonly offersService = inject(OffersService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly router = inject(Router);
  private readonly auth = inject(AuthService);

  private searchParamsChange$ = new Subject<void>();

  ngOnInit(): void {
    this.searchParamsChange$
      .pipe(takeUntilDestroyed(this.destroyRef), debounceTime(1000))
      .subscribe({
        next: () => this.performUrUpdate(),
      });

    if (this.auth.isAuthenticated()) {
      this.autoFillUserFields();
    }

    this.activatedRoute.queryParamMap
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        tap((params) => {
          const amount = params.get('amount');
          const duration = params.get('duration');
          const monthlyIncome = params.get('monthlyIncome');
          const monthlyCosts = params.get('monthlyCosts');
          const age = params.get('age');
          const dependants = params.get('dependants');

          if (amount) this.amount = Number.parseInt(amount);
          if (duration) this.duration = Number.parseInt(duration);
          if (monthlyIncome) this.monthlyIncome = Number.parseInt(monthlyIncome);
          if (monthlyCosts) this.monthlyCosts = Number.parseInt(monthlyCosts);
          if (age) this.age = Number.parseInt(age);
          if (dependants) this.dependants = Number.parseInt(dependants);
        }),
        switchMap(() => {
          if (!this.amount || !this.duration) {
            return of({ offers: [], calculatedOffers: [] });
          }

          if (this.additionalDataProvided()) {
            return this.offersService
              .listCalculatedOffers(
                this.amount,
                this.duration,
                this.monthlyIncome,
                this.monthlyCosts,
                this.age,
                this.dependants,
              )
              .pipe(map((data) => ({ offers: [], calculatedOffers: data })));
          } else {
            return this.offersService
              .listOffers(this.amount, this.duration)
              .pipe(map((data) => ({ offers: data, calculatedOffers: [] })));
          }
        }),
      )
      .subscribe((result: { offers: OfferDto[]; calculatedOffers: CalculatedOfferDto[] }) => {
        this.offers = result.offers;
        this.calculatedOffers = result.calculatedOffers;
      });
  }

  protected autoFillUserFields() {
    this.auth
      .getUserProfile()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (userInfo) => {
          if (!this.age && userInfo.age) {
            this.age = userInfo.age;
          }

          if (!this.dependants && userInfo.dependents) {
            this.dependants = userInfo.dependents;
          }

          if (!this.monthlyIncome && userInfo.income) {
            this.monthlyIncome = userInfo.income;
          }

          if (!this.monthlyCosts && userInfo.costs) {
            this.monthlyCosts = userInfo.costs;
          }

          this.handleParamsChange();
        },
      });
  }

  protected handleParamsChange() {
    this.searchParamsChange$.next();
  }

  protected redirectToOfferDetails(data: { id: number; providerType: ApplicationProviderType }) {
    this.router.navigate([ApplicationRoutes.offer, data.id], {
      queryParams: {
        amount: this.amount,
        duration: this.duration,
        monthlyIncome: this.monthlyIncome,
        monthlyCosts: this.monthlyCosts,
        age: this.age,
        dependants: this.dependants,
        providerType: data.providerType,
      },
    });
  }

  protected additionalDataProvided(): boolean {
    return !!this.monthlyIncome || !!this.monthlyCosts || !!this.age || !!this.dependants;
  }

  protected fetchOffers() {
    if (!this.additionalDataProvided()) {
      this.calculatedOffers = [];
      this.offersService
        .listOffers(this.amount, this.duration)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (offers) => {
            this.offers = offers;
          },
        });
      return;
    }

    this.offers = [];
    this.offersService
      .listCalculatedOffers(
        this.amount,
        this.duration,
        this.monthlyIncome,
        this.monthlyCosts,
        this.age,
        this.dependants,
      )
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({ next: (calculatedOffers) => (this.calculatedOffers = calculatedOffers) });
  }

  private performUrUpdate() {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: {
        amount: this.amount,
        duration: this.duration,
        monthlyIncome: this.monthlyIncome,
        monthlyCosts: this.monthlyCosts,
        age: this.age,
        dependants: this.dependants,
      },
      queryParamsHandling: 'merge',
      replaceUrl: true,
    });
  }
}
