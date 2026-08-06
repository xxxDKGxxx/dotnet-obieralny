import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { apiEndpoints } from '../../api-endpoints';
import { AuditDto } from './audit-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuditService {
  private readonly http = inject(HttpClient);

  listAudits(day: Date): Observable<AuditDto[]> {
    let params = new HttpParams();

    params = params.set('day', day.toISOString());

    return this.http.get<AuditDto[]>(apiEndpoints.audits(), {
      params: params,
    });
  }
}
