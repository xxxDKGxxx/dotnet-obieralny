import { Routes } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { FullSearch } from './full-search/full-search';
import { UserProfileComponent } from './user-profile/user-profile';
import { OfferDetails } from './offer-details/offer-details';
import { MyApplicationsList } from './my-applications-list/my-applications-list';
import {
  applicationDetailsGuard,
  documentUploadGuard,
  isBankEmployeeGuard,
  isRegularUserGuard,
} from './common/route-guards/guards';
import { AllApplicationsList } from './all-applications-list/all-applications-list';
import { ApplicationDetails } from './application-details/application-details';
import { DocumentUpload } from './document-upload/document-upload';

export const ApplicationRoutes = {
  search: 'search',
  userProfile: 'user-profile',
  offer: 'offer',
  myApplications: 'my-applications',
  applications: 'applications',
  applicationDetails: 'application',
  documentUpload: 'upload-document',
};

export const routes: Routes = [
  { path: ApplicationRoutes.search, component: FullSearch },
  { path: `${ApplicationRoutes.offer}/:offerId`, component: OfferDetails },
  { path: '', component: HomePage, pathMatch: 'full' },
  { path: ApplicationRoutes.userProfile, component: UserProfileComponent },
  {
    path: ApplicationRoutes.myApplications,
    component: MyApplicationsList,
    canActivate: [isRegularUserGuard],
  },
  {
    path: ApplicationRoutes.applications,
    component: AllApplicationsList,
    canActivate: [isBankEmployeeGuard],
  },
  {
    path: `${ApplicationRoutes.applicationDetails}/:applicationId`,
    component: ApplicationDetails,
    canActivate: [applicationDetailsGuard],
  },
  {
    path: ApplicationRoutes.documentUpload,
    component: DocumentUpload,
    canActivate: [documentUploadGuard],
  },
];
