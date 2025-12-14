import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuickSearchForm } from './quick-search-form/quick-search-form';
import { CounterService } from '../services/counter.service';

@Component({
  selector: 'app-home-page',
  imports: [QuickSearchForm, CommonModule],
  templateUrl: './home-page.html',
  styleUrls: ['./home-page.css'],
})
export class HomePage {
  private counterService = inject(CounterService);
  counterData$ = this.counterService.getCounterData();
}
