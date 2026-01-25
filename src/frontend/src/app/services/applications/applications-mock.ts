import { ApplicationProviderType } from '../../shared/enum';
import { ApplicationStatus, ApplicationWithProviderTypeDto } from './applications-model';

// --- Aplikacje tego samego użytkownika (Marek Łuszkiewicz) ---
export const userMockApplications: ApplicationWithProviderTypeDto[] = [
  {
    id: 2,
    offerId: 1,
    userId: 2,
    status: ApplicationStatus.Created,
    contactInfo: {
      email: 'marek.l@example.com',
      phoneNumber: '123456789',
      address: 'ul. Programistów 1, Wrocław',
    },
    applicantFinancials: {
      income: 20000,
      costs: 6000,
      dependents: 2,
      job: 'Dev',
    },
    personalData: {
      firstName: 'Marek',
      lastName: 'Łuszkiewicz',
      age: 20,
    },
    offerConditions: {
      amount: 20000,
      duration: 12,
      interestRate: 8,
    },
    documentId: null,
    providerType: ApplicationProviderType.ArdalisBank,
  },
  {
    id: 5,
    offerId: 10,
    userId: 2,
    status: ApplicationStatus.Granted,
    contactInfo: {
      email: 'marek.l@example.com',
      phoneNumber: '123456789',
      address: 'ul. Programistów 1, Wrocław',
    },
    applicantFinancials: {
      income: 20000,
      costs: 6000,
      dependents: 2,
      job: 'Dev',
    },
    personalData: {
      firstName: 'Marek',
      lastName: 'Łuszkiewicz',
      age: 20,
    },
    offerConditions: {
      amount: 50000,
      duration: 24,
      interestRate: 6.5,
    },
    documentId: 'DOC-9921',
    providerType: ApplicationProviderType.ArdalisBank,
  },
  {
    id: 12,
    offerId: 3,
    userId: 2,
    status: ApplicationStatus.Rejected,
    contactInfo: {
      email: 'marek.l@example.com',
      phoneNumber: '123456789',
      address: 'ul. Programistów 1, Wrocław',
    },
    applicantFinancials: {
      income: 20000,
      costs: 6000,
      dependents: 2,
      job: 'Dev',
    },
    personalData: {
      firstName: 'Marek',
      lastName: 'Łuszkiewicz',
      age: 20,
    },
    offerConditions: {
      amount: 5000,
      duration: 6,
      interestRate: 12,
    },
    documentId: null,
    providerType: ApplicationProviderType.ArdalisBank,
  },
];

// --- Wszystkie aplikacje (różni użytkownicy) ---
export const allApplications: ApplicationWithProviderTypeDto[] = [
  ...userMockApplications, // Wrzucamy tu też wnioski Marka
  {
    id: 20,
    offerId: 4,
    userId: 45,
    status: ApplicationStatus.AwaitingSignature,
    contactInfo: {
      email: 'anna.nowak@test.pl',
      phoneNumber: '987654321',
      address: 'ul. Testowa 5, Warszawa',
    },
    applicantFinancials: {
      income: 15000,
      costs: 4000,
      dependents: 0,
      job: 'UX Designer',
    },
    personalData: {
      firstName: 'Anna',
      lastName: 'Nowak',
      age: 28,
    },
    offerConditions: {
      amount: 100000,
      duration: 48,
      interestRate: 5.2,
    },
    documentId: 'DOC-1122',
    providerType: ApplicationProviderType.ArdalisBank,
  },
  {
    id: 21,
    offerId: 7,
    userId: 102,
    status: ApplicationStatus.Created,
    contactInfo: {
      email: 'j.kowalski@poczta.pl',
      phoneNumber: '555666777',
      address: 'Al. Jerozolimskie 10, Warszawa',
    },
    applicantFinancials: {
      income: 8000,
      costs: 3000,
      dependents: 1,
      job: 'Teacher',
    },
    personalData: {
      firstName: 'Jan',
      lastName: 'Kowalski',
      age: 45,
    },
    offerConditions: {
      amount: 15000,
      duration: 12,
      interestRate: 9.0,
    },
    documentId: null,
    providerType: ApplicationProviderType.ArdalisBank,
  },
];
