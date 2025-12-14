import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

export interface CounterData {
  count: number;
  label: string;
}

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  getCounterData(): Observable<CounterData> {
    return of({
      count: 12847,
      label: 'osób zarejestrowało się na naszej platformie',
    });
  }
}
