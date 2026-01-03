import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  ElementRef,
  ViewChild,
  AfterViewInit,
  PLATFORM_ID,
  effect,
} from '@angular/core';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-button',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatMenuModule, MatDividerModule],
  templateUrl: './login-button.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginButton implements OnInit, AfterViewInit {
  private static readonly GOOGLE_BUTTON_RENDER_DELAY = 100;
  private static readonly GOOGLE_SCRIPT_TIMEOUT = 10000;
  private static readonly GOOGLE_SCRIPT_CHECK_INTERVAL = 100;

  @ViewChild('googleButton', { read: ElementRef }) googleButton?: ElementRef<HTMLDivElement>;

  protected readonly authService = inject(AuthService);
  private readonly platformId = inject(PLATFORM_ID);

  protected readonly currentUser = this.authService.currentUser;
  protected readonly isAuthenticated = this.authService.isAuthenticated;

  constructor() {
    effect(() => {
      const isAuth = this.isAuthenticated();
      if (!isAuth && isPlatformBrowser(this.platformId)) {
        globalThis.setTimeout(
          () => this.renderGoogleButton(),
          LoginButton.GOOGLE_BUTTON_RENDER_DELAY,
        );
      }
    });
  }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.initializeGoogle();
    }
  }

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId) && !this.isAuthenticated()) {
      this.renderGoogleButton();
    }
  }

  private initializeGoogle(): void {
    this.waitForGoogleScript().then(() => {
      this.authService.initializeGoogleSignIn((response) => {
        this.handleGoogleResponse(response);
      });
    });
  }

  private renderGoogleButton(): void {
    this.waitForGoogleScript().then(() => {
      globalThis.setTimeout(() => {
        if (this.googleButton?.nativeElement) {
          this.googleButton.nativeElement.innerHTML = '';
          this.authService.renderGoogleButton(this.googleButton.nativeElement);
        }
      }, LoginButton.GOOGLE_BUTTON_RENDER_DELAY);
    });
  }

  protected login(): void {
    if (this.googleButton?.nativeElement) {
      const googleBtn = this.googleButton.nativeElement.querySelector(
        'div[role="button"]',
      ) as HTMLElement;
      if (googleBtn) {
        googleBtn.click();
      }
    }
  }

  protected logout(): void {
    this.authService.logout();
  }

  private waitForGoogleScript(): Promise<void> {
    return new Promise((resolve) => {
      const googleApi = (globalThis as any).google;
      if (typeof googleApi !== 'undefined' && googleApi.accounts) {
        resolve();
        return;
      }

      const checkInterval = globalThis.setInterval(() => {
        const googleApi = (globalThis as any).google;
        if (typeof googleApi !== 'undefined' && googleApi.accounts) {
          globalThis.clearInterval(checkInterval);
          resolve();
        }
      }, LoginButton.GOOGLE_SCRIPT_CHECK_INTERVAL);

      globalThis.setTimeout(() => {
        globalThis.clearInterval(checkInterval);
        resolve();
      }, LoginButton.GOOGLE_SCRIPT_TIMEOUT);
    });
  }

  private handleGoogleResponse(response: any): void {
    this.authService.setUserFromToken(response);
    this.authService.loginWithGoogle(response.credential).subscribe({
      error: (error) => {
        console.error('Login failed:', error);
        this.authService.logout();
      },
    });
  }
}
