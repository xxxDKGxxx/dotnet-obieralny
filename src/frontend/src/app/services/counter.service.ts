import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface CounterResponse {
  count: number;
}

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  private readonly http = inject(HttpClient);

  getCounterData(): Observable<CounterData> {
    return this.http.get<CounterData>(`${environment.apiBaseUrl}/counter/users-count`);
  }
}
