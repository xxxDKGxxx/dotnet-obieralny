import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-full-search',
  imports: [],
  templateUrl: './full-search.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FullSearch implements OnInit {
  protected amount!: number;
  protected duration!: number;

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
}
