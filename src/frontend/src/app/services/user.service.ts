import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
// import { environment } from '../../environments/environment';
import { UserDto } from './auth.model';

export interface UpdateUserProfileRequest {
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
    // To uncomment when backend endpoint is available
    // return this.http.put<UserDto>(`${environment.apiBaseUrl}/users/${userId}`, request);

    // Placeholder implementation
    const mockResponse: UserDto = {
      id: userId,
      email: 'placeholder@example.com',
      firstName: 'Jan',
      lastName: 'Kowalski',
      role: 'Użytkownik',
      address: request.address,
      phone: request.phone,
      job: request.job,
      income: request.income,
      costs: request.costs,
      age: request.age,
      dependents: request.dependents,
    };
    return of(mockResponse);
  }
}
