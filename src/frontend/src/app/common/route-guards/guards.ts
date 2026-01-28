import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { catchError, combineLatest, map, of } from 'rxjs';
import { inject } from '@angular/core';
import { UserRoles } from '../../services/auth.model';
import { ApplicationsService } from '../../services/applications/applications-service';

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
    catchError(() => {
      return of(router.parseUrl('/'));
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
    catchError(() => {
      return of(router.parseUrl('/'));
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

export const applicationDetailsGuard: CanActivateFn = (r) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.isAuthenticated()) {
    return router.parseUrl('/');
  }

  const applicationsService = inject(ApplicationsService);

  const applicationId = r.params['applicationId'];
  const providerType = r.queryParams['providerType'];

  if (!applicationId || !providerType) {
    return router.parseUrl('/');
  }

  const appIdAsNumber = Number.parseInt(applicationId);

  if (isNaN(appIdAsNumber)) {
    return router.parseUrl('/');
  }

  return combineLatest([
    applicationsService.getById(appIdAsNumber, providerType),
    auth.getUserProfile(),
  ]).pipe(
    map(([appDto, userDto]) => {
      if (userDto.role === UserRoles.Employee) {
        return true;
      }

      if (appDto.userId === userDto.id) {
        return true;
      }

      return false;
    }),
    catchError(() => {
      return of(router.parseUrl('/'));
    }),
  );
};

export const documentUploadGuard: CanActivateFn = (r) => {
  const auth = inject(AuthService);
  const applicationsService = inject(ApplicationsService);
  const router = inject(Router);

  const applicationId = r.queryParams['applicationId'];
  const documentId = r.queryParams['documentId'];
  const providerType = r.queryParams['providerType'];

  if (!applicationId || !documentId || !providerType) {
    return router.parseUrl('/');
  }

  const appIdAsNumber = Number.parseInt(applicationId);
  if (isNaN(appIdAsNumber)) {
    return router.parseUrl('/');
  }

  if (!auth.isAuthenticated()) {
    return applicationsService.getById(appIdAsNumber, providerType).pipe(
      map((app) => {
        const isValid = app.userId === null && app.documentId === documentId;
        return isValid ? true : router.parseUrl('/');
      }),
      catchError(() => of(router.parseUrl('/'))),
    );
  }

  return combineLatest([
    auth.getUserProfile(),
    applicationsService.getById(appIdAsNumber, providerType),
  ]).pipe(
    map(([userDto, appDto]) => {
      const isOwner = appDto.userId === userDto.id && appDto.documentId === documentId;
      return isOwner ? true : router.parseUrl('/');
    }),
    catchError(() => of(router.parseUrl('/'))),
  );
};
