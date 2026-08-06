import { ApplicationProviderType } from '../../shared/enum';

export interface OfferDto {
  id: number;
  title: string;
  description: string;
  minAmount: number;
  maxAmount: number;
  minDuration: number;
  maxDuration: number;
  minInterestRate: number;
  maxInterestRate: number;
  validFrom: Date;
  validTo: Date;
  providerType: ApplicationProviderType;
}

export interface CalculatedOfferDto {
  id: number;
  title: string;
  description: string;
  amount: number;
  duration: number;
  interestRate: number;
  validFrom: Date;
  validTo: Date;
  providerType: ApplicationProviderType;
}
