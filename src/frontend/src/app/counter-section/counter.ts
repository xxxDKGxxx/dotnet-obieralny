import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
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
export class Counter implements OnInit {
  private readonly counterService = inject(CounterService);
  protected counterCount!: number;

  ngOnInit(): void {
    this.counterService.getCounterData().subscribe((data) => {
      this.counterCount = data.count;
    });
  }
}
