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

// Pomocnicza stała dla typu dostawcy (dostosuj do swojego enuma)
const DEFAULT_PROVIDER = ApplicationProviderType.ArdalisBank;

export const mockApplications: ApplicationWithProviderTypeDto[] = [
  // 1. Status: Created (Nowy wniosek, jeszcze bez dokumentu)
  {
    id: 101,
    offerId: 1,
    userId: 501,
    status: ApplicationStatus.Created,
    documentId: null,
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Jan',
      lastName: 'Kowalski',
      age: 34,
    },
    contactInfo: {
      email: 'jan.kowalski@example.com',
      phoneNumber: '500100100',
      address: 'ul. Prosta 1, 00-001 Warszawa',
    },
    applicantFinancials: {
      income: 5500,
      costs: 2000,
      dependents: 1,
      job: 'Programista',
    },
    offerConditions: {
      amount: 15000,
      duration: 24,
      interestRate: 8.5,
    },
  },

  // 2. Status: AwaitingSignature (Wniosek zaakceptowany wstępnie, czeka na podpis - jest ID dokumentu)
  {
    id: 102,
    offerId: 2,
    userId: 502,
    status: ApplicationStatus.AwaitingSignature,
    documentId: null,
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Anna',
      lastName: 'Nowak',
      age: 29,
    },
    contactInfo: {
      email: 'anna.nowak@example.com',
      phoneNumber: '600200200',
      address: 'ul. Długa 5, 30-002 Kraków',
    },
    applicantFinancials: {
      income: 4200,
      costs: 1500,
      dependents: 0,
      job: 'Grafik',
    },
    offerConditions: {
      amount: 5000,
      duration: 12,
      interestRate: 9.0,
    },
  },

  // 3. Status: Signed (Podpisany, czeka na weryfikację/decyzję ostateczną)
  {
    id: 103,
    offerId: 5,
    userId: 503,
    status: ApplicationStatus.Signed,
    documentId: 'doc-uuid-5678-signed',
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Piotr',
      lastName: 'Zieliński',
      age: 45,
    },
    contactInfo: {
      email: 'piotr.zielinski@example.com',
      phoneNumber: '700300300',
      address: 'ul. Leśna 12, 80-003 Gdańsk',
    },
    applicantFinancials: {
      income: 8000,
      costs: 3000,
      dependents: 2,
      job: 'Inżynier',
    },
    offerConditions: {
      amount: 50000,
      duration: 48,
      interestRate: 7.5,
    },
  },

  // 4. Status: Granted (Pożyczka udzielona)
  {
    id: 104,
    offerId: 3,
    userId: 504,
    status: ApplicationStatus.Granted,
    documentId: 'doc-uuid-9999-final',
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Maria',
      lastName: 'Wiśniewska',
      age: 60,
    },
    contactInfo: {
      email: 'maria.wisniewska@example.com',
      phoneNumber: '800400400',
      address: 'ul. Polna 7, 60-004 Poznań',
    },
    applicantFinancials: {
      income: 3800,
      costs: 1200,
      dependents: 0,
      job: 'Emerytka',
    },
    offerConditions: {
      amount: 2000,
      duration: 6,
      interestRate: 0,
    },
  },

  // 5. Status: AwaitingAmendments (Wniosek wymaga poprawek/dosłania dokumentów)
  {
    id: 105,
    offerId: 4,
    userId: null, // Użytkownik niezalogowany / gość
    status: ApplicationStatus.AwaitingAmendments,
    documentId: null,
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Tomasz',
      lastName: 'Wójcik',
      age: 25,
    },
    contactInfo: {
      email: 'tomasz.wojcik@example.com',
      phoneNumber: '505606707',
      address: 'ul. Krótka 3, 50-005 Wrocław',
    },
    applicantFinancials: {
      income: 15000, // Podejrzanie wysoki dochód, może dlatego requires amendments
      costs: 1000,
      dependents: 0,
      job: 'Freelancer',
    },
    offerConditions: {
      amount: 100000,
      duration: 60,
      interestRate: 10.5,
    },
  },

  // 6. Status: Rejected (Wniosek odrzucony)
  {
    id: 106,
    offerId: 1,
    userId: 506,
    status: ApplicationStatus.Rejected,
    documentId: null,
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Katarzyna',
      lastName: 'Lewandowska',
      age: 22,
    },
    contactInfo: {
      email: 'kasia.lew@example.com',
      phoneNumber: '666777888',
      address: 'ul. Szkolna 2, 10-006 Olsztyn',
    },
    applicantFinancials: {
      income: 2000,
      costs: 1900, // Zdolność kredytowa zerowa
      dependents: 0,
      job: 'Student',
    },
    offerConditions: {
      amount: 20000,
      duration: 24,
      interestRate: 12.0,
    },
  },

  // 7. Status: Withdrawn (Wniosek wycofany przez klienta)
  {
    id: 107,
    offerId: 2,
    userId: 507,
    status: ApplicationStatus.Withdrawn,
    documentId: 'doc-uuid-old-111',
    providerType: DEFAULT_PROVIDER,
    personalData: {
      firstName: 'Michał',
      lastName: 'Kamiński',
      age: 40,
    },
    contactInfo: {
      email: 'michal.kaminski@example.com',
      phoneNumber: '999888777',
      address: 'ul. Słoneczna 8, 40-007 Katowice',
    },
    applicantFinancials: {
      income: 6000,
      costs: 2500,
      dependents: 2,
      job: 'Kierowca',
    },
    offerConditions: {
      amount: 10000,
      duration: 12,
      interestRate: 8.0,
    },
  },
];
