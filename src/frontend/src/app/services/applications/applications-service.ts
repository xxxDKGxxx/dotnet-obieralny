import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ApplicationWithProviderTypeDto, PostApplicationRequest } from './applications-model';
import { apiEndpoints } from '../../api-endpoints';
import { Observable } from 'rxjs';
import { ApplicationProviderType } from '../../shared/enum';

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
    let params = new HttpParams();

    if (userId != null) {
      params = params.set('userId', userId);
    }

    if (providerType != null) {
      params = params.set('providerType', providerType);
    }

    return this.http.get<ApplicationWithProviderTypeDto[]>(apiEndpoints.listApplications(), {
      params: params,
    });
  }
}
