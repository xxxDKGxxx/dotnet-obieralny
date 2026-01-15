import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { FullSearch } from './full-search/full-search';
import { UserProfileComponent } from './user-profile/user-profile';
import { OfferDetails } from './offer-details/offer-details';

export const ApplicationRoutes = {
  search: 'search',
  userProfile: 'user-profile',
  offer: 'offer',
};

export const routes: Routes = [
  { path: ApplicationRoutes.search, component: FullSearch },
  { path: `${ApplicationRoutes.offer}/:offerId`, component: OfferDetails },
  { path: '', component: HomePage, pathMatch: 'full' },
  { path: ApplicationRoutes.userProfile, component: UserProfileComponent },
];
