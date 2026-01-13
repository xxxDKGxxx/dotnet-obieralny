import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { FullSearch } from './full-search/full-search';
import { UserProfileComponent } from './user-profile/user-profile';

export const ApplicationRoutes = {
  search: 'search',
  userProfile: 'user-profile',
};

export const routes: Routes = [
  { path: ApplicationRoutes.search, component: FullSearch },
  { path: '', component: HomePage, pathMatch: 'full' },
  { path: ApplicationRoutes.userProfile, component: UserProfileComponent },
];
