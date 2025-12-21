import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { CounterService } from '../services/counter.service';

@Component({
  selector: 'app-counter',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './counter.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Counter {
  private counterService = inject(CounterService);
  protected counterCount = signal<number | null>(null);

  constructor() {
    this.counterService.getCounterData().subscribe(data => {
      this.counterCount.set(data.count);
    });
  }
}
