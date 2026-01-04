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

  getCounterData(): Observable<CounterResponse> {
    return this.http.get<CounterResponse>(`${environment.apiBaseUrl}/counter`);
  }
}
