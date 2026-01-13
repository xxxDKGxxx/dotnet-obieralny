import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { debounceTime, Subject } from 'rxjs';

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
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ApplicationForm implements OnInit {
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

  private inputChangeSubject = new Subject<OfferConditions>();

  ngOnInit(): void {
    this.inputChangeSubject.pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (conditions) => {
        this.conditionsChange.emit(conditions);
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

  protected applyForOffer() {}
}
