import {
  Component,
  inject,
  OnInit,
  PLATFORM_ID,
  ChangeDetectorRef,
  DestroyRef,
} from '@angular/core';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { Observable, interval, takeWhile, take, of, map } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';
import { UserDto } from '../services/auth.model';

@Component({
  selector: 'app-login-button',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatMenuModule, MatDividerModule],
  templateUrl: './login-button.html',
})
export class LoginButton implements OnInit {
  private static readonly GOOGLE_SCRIPT_TIMEOUT = 10000;
  private static readonly GOOGLE_SCRIPT_CHECK_INTERVAL = 100;

  protected readonly authService = inject(AuthService);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly snackBar = inject(MatSnackBar);
  private readonly cdr = inject(ChangeDetectorRef);
  private readonly destroyRef = inject(DestroyRef);

  protected currentUser: UserDto | null = null;
  protected readonly isAuthenticated = this.authService.isAuthenticated;

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.initializeGoogle();
    }
    if (this.isAuthenticated()) {
      this.authService
        .getUserProfile()
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (userInfo) => {
            this.currentUser = userInfo;
            this.cdr.markForCheck();
          },
          error: (error) => {
            console.error('Failed to load user profile:', error);
            if (error.status === 401) {
              this.authService.logout();
            }
          },
        });
    }
  }

  private initializeGoogle(): void {
    this.waitForGoogleScript()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.authService.initializeGoogleSignIn((response) => {
          this.handleGoogleResponse(response);
        });
      });
  }

  protected login(): void {
    this.authService.triggerGoogleLogin();
  }

  protected logout(): void {
    this.authService.logout();
    this.currentUser = null;
    this.cdr.markForCheck();
  }

  private waitForGoogleScript(): Observable<void> {
    const googleApi = (globalThis as any).google;
    if (typeof googleApi !== 'undefined' && googleApi.accounts) {
      return of(undefined);
    }

    return interval(LoginButton.GOOGLE_SCRIPT_CHECK_INTERVAL).pipe(
      takeWhile(() => {
        const googleApi = (globalThis as any).google;
        return typeof googleApi === 'undefined' || !googleApi.accounts;
      }, true),
      take(LoginButton.GOOGLE_SCRIPT_TIMEOUT / LoginButton.GOOGLE_SCRIPT_CHECK_INTERVAL),
      map(() => undefined),
    );
  }

  private handleGoogleResponse(response: any): void {
    this.authService
      .loginWithGoogle(response.credential)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.authService
            .getUserProfile()
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe({
              next: (userInfo) => {
                this.currentUser = userInfo;
                this.cdr.markForCheck();
              },
              error: (error) => {
                console.error('Failed to load user profile after login:', error);
              },
            });
        },
        error: (error) => {
          console.error('Login failed:', error);
          this.authService.logout();
          this.snackBar.open('Logowanie nie powiodło się. Spróbuj ponownie.', 'Zamknij', {
            duration: 5000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
            panelClass: ['error-snackbar'],
          });
        },
      });
  }
}
