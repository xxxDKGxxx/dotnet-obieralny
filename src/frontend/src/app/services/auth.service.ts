import { Injectable, inject, signal, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserInfo, GoogleAuthResponse, GoogleCredentialResponse, JwtPayload } from './auth.model';

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

  private googleCallback: ((response: GoogleCredentialResponse) => void) | null = null;
  private googleButtonElement: HTMLElement | null = null;

  initializeGoogleSignIn(callback: (response: GoogleCredentialResponse) => void): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    this.googleCallback = callback;

    const googleApi = (globalThis as any).google;
    if (typeof googleApi !== 'undefined' && googleApi.accounts?.id) {
      googleApi.accounts.id.initialize({
        client_id: environment.googleClientId,
        callback: (response: any) => {
          if (this.googleCallback) {
            this.googleCallback(response);
          }
        },
      });

      if (!this.googleButtonElement) {
        const container = globalThis.document.createElement('div');
        container.id = 'google-signin-hidden';
        container.style.cssText = 'position: absolute; top: -9999px; left: -9999px;';
        globalThis.document.body.appendChild(container);

        googleApi.accounts.id.renderButton(container, {
          theme: 'outline',
          size: 'large',
        });

        this.googleButtonElement = container;
      }
    }
  }

  triggerGoogleLogin(): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    if (this.googleButtonElement) {
      const button = this.googleButtonElement.querySelector('div[role="button"]') as HTMLElement;
      if (button) {
        button.click();
      }
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
      if (googleApi?.accounts?.id && this.currentUser()?.email) {
        googleApi.accounts.id.revoke(this.currentUser()!.email, () => {});
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

  // TODO: Remove this method when /users/me endpoint is available
  // Currently using data from Google token - should fetch from backend database instead
  setUserFromToken(credential: GoogleCredentialResponse): void {
    const payload = this.parseJwt(credential.credential);
    if (payload) {
      this.currentUser.set({
        email: payload.email ?? '',
        firstName: payload.given_name ?? '',
        lastName: payload.family_name ?? '',
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

  // TODO: Replace with call to /users/me endpoint when available
  // Currently parsing JWT on frontend - user data should come from backend database
  private restoreUserFromToken(): void {
    const token = this.getToken();
    if (token) {
      const payload = this.parseJwt(token);
      if (payload) {
        this.currentUser.set({
          email: payload.email ?? '',
          firstName: payload.given_name ?? '',
          lastName: payload.family_name ?? '',
        });
      }
    }
  }

  // TODO: Remove this method when /users/me endpoint is available
  private parseJwt(token: string): JwtPayload | null {
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
      return JSON.parse(jsonPayload) as JwtPayload;
    } catch (error) {
      console.error('Failed to parse JWT token:', error);
      return null;
    }
  }
}
