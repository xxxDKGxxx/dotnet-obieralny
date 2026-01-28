import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CounterResponse } from './counter-model';
import { apiEndpoints } from '../../api-endpoints';

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  private readonly http = inject(HttpClient);

  getCounterData(): Observable<CounterResponse> {
    return this.http.get<CounterResponse>(apiEndpoints.counter());
  }
}
