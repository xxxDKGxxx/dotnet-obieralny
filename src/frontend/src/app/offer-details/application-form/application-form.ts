import { Component, DestroyRef, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Observable, Subject, switchMap } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { ApplicationsService } from '../../services/applications/applications-service';
import { ApplicationWithProviderTypeDto } from '../../services/applications/applications-model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApplicationProviderType } from '../../shared/enum';

export interface OfferConditions {
  amount: number | null;
  duration: number | null;
  monthlyIncome: number | null;
  monthlyCosts: number | null;
  age: number | null;
  dependants: number | null;
}

@Component({
  selector: 'app-application-form',
  imports: [
    FormsModule,
    MatDividerModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './application-form.html',
})
export class ApplicationForm implements OnInit {
  @Input({ required: true })
  offerId!: number;
  @Input({ required: true })
  providerType!: ApplicationProviderType;
  @Input()
  amount!: number;
  @Input()
  duration!: number;
  @Input()
  monthlyIncome!: number;
  @Input()
  monthlyCosts!: number;
  @Input()
  age!: number;
  @Input()
  dependants!: number;

  @Output()
  conditionsChange = new EventEmitter<OfferConditions>();

  protected name!: string;
  protected surname!: string;
  protected email!: string;
  protected address!: string;
  protected phoneNumber!: string;
  protected job!: string;

  private readonly destroyRef = inject(DestroyRef);
  private readonly auth = inject(AuthService);
  private readonly applicationsService = inject(ApplicationsService);
  private readonly snackBar = inject(MatSnackBar);

  private inputChangeSubject = new Subject<OfferConditions>();

  ngOnInit(): void {
    this.inputChangeSubject.pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (conditions) => {
        this.conditionsChange.emit(conditions);
      },
    });

    if (this.auth.isAuthenticated()) {
      this.autoFillUserFields();
    }
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

          if (!this.address && userInfo.address) {
            this.address = userInfo.address;
          }

          if (!this.name && userInfo.firstName) {
            this.name = userInfo.firstName;
          }

          if (!this.surname && userInfo.lastName) {
            this.surname = userInfo.lastName;
          }

          if (!this.email && userInfo.email) {
            this.email = userInfo.email;
          }

          if (!this.job && userInfo.job) {
            this.job = userInfo.job;
          }

          if (!this.phoneNumber && userInfo.phone) {
            this.phoneNumber = userInfo.phone;
          }
        },
      });
  }

  protected outputChangedOfferConditions() {
    this.inputChangeSubject.next({
      amount: this.amount,
      duration: this.duration,
      monthlyIncome: this.monthlyIncome,
      monthlyCosts: this.monthlyCosts,
      age: this.age,
      dependants: this.dependants,
    });
  }

  protected applyForOffer() {
    this.createApplicationBasedOnLoginStatus().subscribe({
      next: (_) => {
        this.snackBar.open('Pomyślnie utworzono aplikację!');
      },
      error: (e) => {
        this.snackBar.open('Wystąpił błąd podczas tworzenia aplikacji. Spróbuj ponownie później.');
        console.error(e);
      },
    });
  }

  private createApplicationBasedOnLoginStatus(): Observable<ApplicationWithProviderTypeDto> {
    if (this.auth.isAuthenticated()) {
      return this.auth.getUserProfile().pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap((val) => {
          return this.applicationsService.createApplication({
            offerId: this.offerId,
            userId: val.id,
            amount: this.amount,
            duration: this.duration,
            financials: {
              income: this.monthlyIncome,
              costs: this.monthlyCosts,
              dependents: this.dependants,
              job: this.job,
            },
            contact: {
              email: this.email,
              phoneNumber: this.phoneNumber,
              address: this.address,
            },
            personalData: {
              firstName: this.name,
              lastName: this.surname,
              age: this.age,
            },
            providerType: this.providerType,
          });
        }),
      );
    }
    return this.applicationsService
      .createApplication({
        offerId: this.offerId,
        userId: null,
        amount: this.amount,
        duration: this.duration,
        financials: {
          income: this.monthlyIncome,
          costs: this.monthlyCosts,
          dependents: this.dependants,
          job: this.job,
        },
        contact: {
          email: this.email,
          phoneNumber: this.phoneNumber,
          address: this.address,
        },
        personalData: {
          firstName: this.name,
          lastName: this.surname,
          age: this.age,
        },
        providerType: this.providerType,
      })
      .pipe(takeUntilDestroyed(this.destroyRef));
  }
}
