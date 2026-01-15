import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

export interface CounterResponse {
  count: number;
}

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  private readonly http = inject(HttpClient);

  getCounterData(): Observable<CounterResponse> {
    return of({ count: 42 });
  }
}
