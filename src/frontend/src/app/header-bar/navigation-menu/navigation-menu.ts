import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApplicationRoutes } from '../../app.routes';
import { AuthService } from '../../services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserDto, UserRoles } from '../../services/auth.model';

@Component({
  selector: 'app-navigation-menu',
  imports: [RouterLink],
  templateUrl: './navigation-menu.html',
  styleUrl: './navigation-menu.css',
})
export class NavigationMenu implements OnInit {
  protected readonly searchPath = ApplicationRoutes.search;
  protected readonly myApplicationsPath = ApplicationRoutes.myApplications;
  protected readonly applicationsPath = ApplicationRoutes.applications;

  protected user: UserDto | null = null;

  private readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.authService
        .getUserProfile()
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (user) => {
            this.user = user;
          },
        });
    }
  }

  protected isEmployee(): boolean {
    return !!this.user && this.user.role == UserRoles.Employee;
  }

  protected isRegularUser(): boolean {
    return !!this.user && this.user.role == UserRoles.User;
  }
}
