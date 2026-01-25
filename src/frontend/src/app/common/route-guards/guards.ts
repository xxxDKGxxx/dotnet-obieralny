import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { map } from 'rxjs';
import { inject } from '@angular/core';
import { UserRoles } from '../../services/auth.model';

export const isRegularUserGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return router.parseUrl('/');
  }
  return auth.getUserProfile().pipe(
    map((user) => {
      if (user && user.role == UserRoles.User) {
        return true;
      }
      return router.parseUrl('/');
    }),
  );
};

export const isBankEmployeeGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return router.parseUrl('/');
  }
  return auth.getUserProfile().pipe(
    map((user) => {
      if (user && user.role == UserRoles.Employee) {
        return true;
      }
      return router.parseUrl('/');
    }),
  );
};
