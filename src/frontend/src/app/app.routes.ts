import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { FullSearch } from './full-search/full-search';

export const ApplicationRoutes = {
  search: 'search',
};

export const routes: Routes = [
  { path: ApplicationRoutes.search, component: FullSearch },
  { path: '', component: HomePage, pathMatch: 'full' },
];
