import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { CounterResponse } from './counter-model';

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  private readonly http = inject(HttpClient);

  getCounterData(): Observable<CounterResponse> {
    return of({ count: 42 });
  }
}
