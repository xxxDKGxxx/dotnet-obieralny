export interface UserInfo {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  address?: string;
  phone?: string;
  job?: string;
  income?: number;
  costs?: number;
  age?: number;
  dependents?: number;
}

export interface GoogleAuthResponse {
  accessToken: string;
}

export interface GoogleCredentialResponse {
  credential: string;
}
