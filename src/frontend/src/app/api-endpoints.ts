import { environment } from '../environments/environment';

export const apiEndpoints = {
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
};
