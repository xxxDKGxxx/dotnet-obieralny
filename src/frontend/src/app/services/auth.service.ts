import { Injectable, inject, signal, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface UserInfo {
  email: string;
  firstName: string;
  lastName: string;
}

export interface GoogleAuthResponse {
  accessToken: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly tokenKey = 'auth_token';

  public readonly isAuthenticated = signal<boolean>(this.hasToken());
  public readonly currentUser = signal<UserInfo | null>(null);

  constructor() {
    this.restoreUserFromToken();
  }

  initializeGoogleSignIn(callback: (response: any) => void): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const googleApi = (globalThis as any).google;
    if (typeof googleApi !== 'undefined' && googleApi.accounts) {
      googleApi.accounts.id.initialize({
        client_id: environment.googleClientId,
        callback,
      });
    }
  }

  renderGoogleButton(element: HTMLElement): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const googleApi = (globalThis as any).google;
    if (typeof googleApi !== 'undefined' && googleApi.accounts) {
      googleApi.accounts.id.renderButton(element, {
        theme: 'outline',
        size: 'large',
        text: 'signin_with',
        shape: 'rectangular',
        locale: 'pl',
      });
    }
  }

  loginWithGoogle(googleToken: string): Observable<GoogleAuthResponse> {
    return this.http
      .post<GoogleAuthResponse>(`${environment.apiBaseUrl}/api/v1/auth/google-login`, {
        token: googleToken,
      })
      .pipe(
        tap((response) => {
          this.saveToken(response.accessToken);
        }),
      );
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      globalThis.localStorage?.removeItem(this.tokenKey);
      const googleApi = (globalThis as any).google;
      if (typeof googleApi !== 'undefined' && googleApi.accounts) {
        googleApi.accounts.id.disableAutoSelect();
      }
    }
    this.isAuthenticated.set(false);
    this.currentUser.set(null);
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return globalThis.localStorage?.getItem(this.tokenKey) ?? null;
    }
    return null;
  }

  setUserFromToken(credential: any): void {
    const payload = this.parseJwt(credential.credential);
    if (payload) {
      this.currentUser.set({
        email: payload.email,
        firstName: payload.given_name || '',
        lastName: payload.family_name || '',
      });
    }
  }

  private saveToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      globalThis.localStorage?.setItem(this.tokenKey, token);
    }
    this.isAuthenticated.set(true);
    this.restoreUserFromToken();
  }

  private hasToken(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!globalThis.localStorage?.getItem(this.tokenKey);
    }
    return false;
  }

  private restoreUserFromToken(): void {
    const token = this.getToken();
    if (token) {
      const payload = this.parseJwt(token);
      if (payload) {
        this.currentUser.set({
          email: payload.email || '',
          firstName: payload.given_name || '',
          lastName: payload.family_name || '',
        });
      }
    }
  }

  private parseJwt(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        globalThis
          .atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join(''),
      );
      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error('Failed to parse JWT token:', error);
      return null;
    }
  }
}
