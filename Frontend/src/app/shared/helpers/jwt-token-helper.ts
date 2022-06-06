import { JwtHelperService } from '@auth0/angular-jwt';

import { JwtTokenInfo } from '../models/jwt-token-info';

export class JwtTokenHelper {
  static localStorageJwtTokenKey = 'jwt-token';

  static getJwtToken(): string | null {
    return localStorage.getItem(JwtTokenHelper.localStorageJwtTokenKey);
  }

  static setJwtToken(jwtToken: string | null): void {
    if (jwtToken === null) {
      localStorage.removeItem(JwtTokenHelper.localStorageJwtTokenKey);
      return;
    }

    localStorage.setItem(JwtTokenHelper.localStorageJwtTokenKey, jwtToken);
  }

  static parseJwtToken(jwtToken: string): JwtTokenInfo {
    const decoded = new JwtHelperService().decodeToken(jwtToken) as {
      exp: number;
      username: string;
      email: string;
    };
    return {
      validTo: decoded.exp * 1000, // jwt expires is in seconds (not milliseconds!) since epoch
      username: decoded.username,
      email: decoded.email,
    };
  }
}
