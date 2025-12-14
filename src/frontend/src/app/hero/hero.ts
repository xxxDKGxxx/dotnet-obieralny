import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { CounterService } from '../services/counter.service';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './hero.html',
  styleUrls: ['./hero.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Hero {
  private counterService = inject(CounterService);
  counterData$ = this.counterService.getCounterData();
}
