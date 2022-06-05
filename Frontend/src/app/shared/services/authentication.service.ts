import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { IdentityService } from '../api/services';
import { JwtTokenInfo } from '../models/jwt-token-info';

const localStorageJwtTokenInfoKey = 'jwtTokenInfo';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private token$: BehaviorSubject<JwtTokenInfo | null>;
  readonly token: Observable<JwtTokenInfo | null>;

  constructor(
    private readonly identityService: IdentityService,
    private readonly router: Router
  ) {
    const currentToken = this.parsedLocalStorageToken;
    this.token$ = new BehaviorSubject<JwtTokenInfo | null>(
      currentToken && currentToken.validTo > new Date().valueOf()
        ? currentToken
        : null
    );
    this.token = this.token$
      .asObservable()
      .pipe(
        tap((jwtTokenInfo) =>
          this.updateJwtTokenInfoInLocalStorage(jwtTokenInfo)
        )
      );
  }

  get tokenValue(): JwtTokenInfo | null {
    return this.token$.value;
  }

  get loggedIn(): Observable<boolean> {
    return this.token.pipe(map((token) => token !== null));
  }

  login(email: string, password: string): Observable<void> {
    return this.identityService
      .identityLogin({ body: { email, password } })
      .pipe(
        map((userLoginResponse) => {
          const decoded = new JwtHelperService().decodeToken(
            userLoginResponse.token!
          ) as { exp: number; username: string; email: string };
          var jwtTokenInfo = {
            raw: userLoginResponse.token,
            validTo: decoded.exp * 1000, // jwt expires is in seconds (not milliseconds!) since epoch
            username: decoded.username,
            email: decoded.email,
          } as JwtTokenInfo;
          this.token$.next(jwtTokenInfo);
        })
      );
  }

  logout(): void {
    this.token$.next(null);
    this.router.navigate(['/']);
  }

  private updateJwtTokenInfoInLocalStorage(jwtTokenInfo: JwtTokenInfo | null) {
    if (jwtTokenInfo === null) {
      localStorage.removeItem(localStorageJwtTokenInfoKey);
      return;
    }

    localStorage.setItem(
      localStorageJwtTokenInfoKey,
      JSON.stringify(jwtTokenInfo)
    );
  }

  private get parsedLocalStorageToken(): JwtTokenInfo | null {
    const currentToken = localStorage.getItem(localStorageJwtTokenInfoKey);
    return currentToken ? (JSON.parse(currentToken) as JwtTokenInfo) : null;
  }
}
