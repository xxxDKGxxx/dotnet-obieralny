export interface UserDto {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  role: UserRoles;
  address?: string;
  phone?: string;
  job?: string;
  income?: number;
  costs?: number;
  age?: number;
  dependents?: number;
}

export enum UserRoles {
  User = 'User',
  Employee = 'Employee',
  Admin = 'Admin',
}

export interface GoogleAuthResponse {
  accessToken: string;
}

export interface GoogleCredentialResponse {
  credential: string;
}
