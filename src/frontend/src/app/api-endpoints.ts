import { environment } from '../environments/environment';

export const apiEndpoints = {
  postApplication: () => `${environment.apiBaseUrl}/applications`,
  listOffers: (amount: number, duration: number) =>
    `${environment.apiBaseUrl}/offers?amount=${amount}&duration=${duration}`,
  listCalculatedOffers: (
    amount: number,
    duration: number,
    monthlyIncome: number,
    monthlyCosts: number,
    age: number,
    dependants: number,
  ) =>
    `${environment.apiBaseUrl}/calculated-offers?amount=${amount}&duration=${duration}&monthlyIncome=${monthlyIncome}&monthlyCosts=${monthlyCosts}&age=${age}&dependants=${dependants}`,
  userProfile: (userId: number) => `${environment.apiBaseUrl}/users/${userId}`,
  getOfferById: (id: number, providerType: string) =>
    `${environment.apiBaseUrl}/offers/${id}?providerType=${providerType}`,
  getCalculatedOfferById: (
    id: number,
    amount: number,
    duration: number,
    monthlyIncome: number,
    monthlyCosts: number,
    age: number,
    dependants: number,
    providerType: string,
  ) =>
    `${environment.apiBaseUrl}/calculated-offers/${id}?amount=${amount}&duration=${duration}&monthlyIncome=${monthlyIncome}&monthlyCosts=${monthlyCosts}&age=${age}&dependants=${dependants}&providerType=${providerType}`,
  listApplications: () => `${environment.apiBaseUrl}/applications`,
};
