import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface CounterData {
  count: number;
}

@Injectable({
  providedIn: 'root',
})
export class CounterService {
  private readonly http = inject(HttpClient);

  getCounterData(): Observable<CounterData> {
    const url = `${environment.apiBaseUrl}/api/counter/users-count`;
    return this.http.get<CounterData>(url);
  }
}
