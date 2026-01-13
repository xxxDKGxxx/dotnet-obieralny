import { Injectable, inject, signal, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserDto, GoogleAuthResponse, GoogleCredentialResponse } from './auth.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly router = inject(Router);
  private readonly tokenKey = 'auth_token';

  public readonly isAuthenticated = signal<boolean>(false);

  constructor() {
    if (this.hasToken()) {
      this.isAuthenticated.set(true);
    }
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
      .post<GoogleAuthResponse>(`${environment.apiBaseUrl}/auth/google-login`, {
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
    }
    this.isAuthenticated.set(false);
    this.router.navigate(['/']);
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return globalThis.localStorage?.getItem(this.tokenKey) ?? null;
    }
    return null;
  }

  getUserProfile(): Observable<UserDto> {
    return this.http.get<UserDto>(`${environment.apiBaseUrl}/users/me`);
  }

  private saveToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      globalThis.localStorage?.setItem(this.tokenKey, token);
    }
    this.isAuthenticated.set(true);
  }

  private hasToken(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!globalThis.localStorage?.getItem(this.tokenKey);
    }
    return false;
  }
}
