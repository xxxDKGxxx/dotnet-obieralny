import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApplicationWithProviderTypeDto, PostApplicationRequest } from './applications-model';
import { apiEndpoints } from '../../api-endpoints';
import { Observable, of } from 'rxjs';
import { allApplications, userMockApplications } from './applications-mock';
import { ApplicationProviderType } from '../offers/offer-model';

@Injectable({
  providedIn: 'root',
})
export class ApplicationsService {
  private readonly http = inject(HttpClient);

  createApplication(request: PostApplicationRequest): Observable<ApplicationWithProviderTypeDto> {
    return this.http.post<ApplicationWithProviderTypeDto>(apiEndpoints.postApplication(), request);
  }

  listApplications(
    userId: number | null,
    providerType: ApplicationProviderType | null,
  ): Observable<ApplicationWithProviderTypeDto[]> {
    void providerType;

    if (userId) {
      return of(userMockApplications);
    }

    return of(allApplications);
  }
}
