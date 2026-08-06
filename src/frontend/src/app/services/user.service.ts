import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDto } from './auth.model';
import { apiEndpoints } from '../api-endpoints';

export interface UpdateUserProfileRequest {
  firstName?: string;
  lastName?: string;
  address?: string;
  phone?: string;
  job?: string;
  income?: number;
  costs?: number;
  age?: number;
  dependents?: number;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly http = inject(HttpClient);

  updateProfile(userId: number, request: UpdateUserProfileRequest): Observable<UserDto> {
    return this.http.put<UserDto>(apiEndpoints.userProfile(userId), request);
  }
}
