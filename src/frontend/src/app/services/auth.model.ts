export interface UserInfo {
  email: string;
  firstName: string;
  lastName: string;
}

export interface GoogleAuthResponse {
  accessToken: string;
}

export interface GoogleCredentialResponse {
  credential: string;
}

export interface JwtPayload {
  email?: string;
  given_name?: string;
  family_name?: string;
  exp?: number;
  iat?: number;
}
