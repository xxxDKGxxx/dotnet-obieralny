import { ApplicationProviderType } from '../../shared/enum';

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
  status: ApplicationStatus;
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

export interface PutApplicationStatusRequest {
  newStatus: string;
  providerType: string;
  statusChangeMessage: string | null;
}

export enum ApplicationStatus {
  Created = 'Created',
  AwaitingSignature = 'AwaitingSignature',
  Signed = 'Signed',
  Granted = 'Granted',
  AwaitingAmendments = 'AwaitingAmendments',
  Rejected = 'Rejected',
  Withdrawn = 'Withdrawn',
}
