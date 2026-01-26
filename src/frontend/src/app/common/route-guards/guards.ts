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

export const isLoggedIn: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return router.parseUrl('/');
  }

  return true;
};

// TODO Implememnt when getApplicationById is available
// export const documentUploadGuard: CanActivateFn = (r, s) => {
//   const auth = inject(AuthService);
//   const applicationsService = inject(ApplicationsService);
//   const router = inject(Router);

//   const applicationId = r.queryParams['applicationId'];
//   const documentId = r.queryParams['documentId'];

//   if (!applicationId || !documentId) {
//     return router.parseUrl('/');
//   }

//   const applicationIdAsNumber = Number.parseInt(applicationId);

//   if (!applicationIdAsNumber) {
//     return router.parseUrl('/');
//   }
// };
