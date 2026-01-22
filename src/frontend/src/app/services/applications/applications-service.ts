import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApplicationWithProviderTypeDto, PostApplicationRequest } from './applications-model';
import { apiEndpoints } from '../../api-endpoints';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApplicationsService {
  private readonly http = inject(HttpClient);

  createApplication(request: PostApplicationRequest): Observable<ApplicationWithProviderTypeDto> {
    return this.http.post<ApplicationWithProviderTypeDto>(apiEndpoints.postApplication(), request);
  }
}
