import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { FullSearch } from './full-search/full-search';
import { OfferDetails } from './offer-details/offer-details';

export const ApplicationRoutes = {
  search: 'search',
  offer: 'offer',
};

export const routes: Routes = [
  { path: ApplicationRoutes.search, component: FullSearch },
  { path: `${ApplicationRoutes.offer}/:offerId`, component: OfferDetails },
  { path: '', component: HomePage, pathMatch: 'full' },
];
