import { Component, inject, OnInit, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CounterService } from '../services/counter/counter.service';

@Component({
  selector: 'app-counter',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './counter.html',
})
export class Counter implements OnInit {
  private readonly counterService = inject(CounterService);
  private readonly destroyRef = inject(DestroyRef);
  protected counterCount!: number;

  ngOnInit(): void {
    this.counterService
      .getCounterData()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => {
        this.counterCount = data.count;
      });
  }
}
