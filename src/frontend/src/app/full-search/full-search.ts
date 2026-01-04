import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

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
  ],
  templateUrl: './full-search.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FullSearch implements OnInit {
  protected amount!: number;
  protected duration!: number;
  protected monthlyIncome!: number;
  protected monthlyCosts!: number;
  protected age!: number;
  protected dependants!: number;

  private readonly activatedRoute = inject(ActivatedRoute);

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe((params) => {
      const amount = params.get('amount');
      const duration = params.get('duration');

      if (!amount || !duration) {
        return;
      }

      const amountAsNumber = Number.parseInt(amount);
      const durationAsNumber = Number.parseInt(duration);

      this.amount = amountAsNumber;
      this.duration = durationAsNumber;
    });
  }

  protected additionalDataProvided(): boolean {
    return !!this.monthlyIncome || !!this.monthlyCosts || !!this.age || !!this.dependants;
  }

  protected fetchCalculatedOffers() {
    // TODO
  }
}
