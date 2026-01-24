import { ApplicationProviderType } from '../offers/offer-model';

export interface ApplicantContactInfo {
  email: string;
  phoneNumber: string;
  address: string;
}

export interface ApplicantFinancialInfo {
  income: number;
  costs: number;
  dependents: number;
  job: string;
}

export interface ApplicantPersonalInfo {
  firstName: string;
  lastName: string;
  age: number;
}

export interface OfferConditions {
  amount: number;
  duration: number;
  interestRate: number;
}

export interface ApplicationWithProviderTypeDto {
  id: number;
  offerId: number;
  userId: number | null;
  status: string;
  contactInfo: ApplicantContactInfo;
  applicantFinancials: ApplicantFinancialInfo;
  personalData: ApplicantPersonalInfo;
  offerConditions: OfferConditions;
  documentId: string | null;
  providerType: ApplicationProviderType;
}
export interface PostApplicationRequest {
  offerId: number;
  userId: number | null;
  amount: number;
  duration: number;
  financials: ApplicantFinancialInfo;
  contact: ApplicantContactInfo;
  personalData: ApplicantPersonalInfo;
  providerType: ApplicationProviderType;
}
