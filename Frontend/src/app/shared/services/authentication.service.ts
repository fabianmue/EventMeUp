import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { IdentityService } from '../api/services';
import { JwtTokenHelper } from '../helpers/jwt-token-helper';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private jwtToken$: BehaviorSubject<string | null>;
  readonly jwtToken: Observable<string | null>;

  constructor(
    private readonly identityService: IdentityService,
    private readonly router: Router
  ) {
    this.jwtToken$ = this.initializeJwtToken();
    this.jwtToken = this.jwtToken$
      .asObservable()
      .pipe(tap(JwtTokenHelper.setJwtToken));
  }

  get tokenValue(): string | null {
    return this.jwtToken$.value;
  }

  get loggedIn(): Observable<boolean> {
    return this.jwtToken.pipe(map((token) => token !== null));
  }

  get username(): Observable<string | null> {
    return this.jwtToken.pipe(
      map((token) =>
        token === null ? null : JwtTokenHelper.parseJwtToken(token).username
      )
    );
  }

  login(email: string, password: string): Observable<void> {
    return this.identityService
      .identityLogin({ body: { email, password } })
      .pipe(
        map((userLoginResponse) => {
          this.jwtToken$.next(userLoginResponse.token!);
        })
      );
  }

  logout(): void {
    this.jwtToken$.next(null);
    this.router.navigate(['/']);
  }

  private initializeJwtToken(): BehaviorSubject<string | null> {
    var jwtTokenInitialValue: string | null = null;
    const jwtToken = JwtTokenHelper.getJwtToken();
    if (jwtToken !== null) {
      const parsed = JwtTokenHelper.parseJwtToken(jwtToken);

      if (parsed.validTo > new Date().valueOf()) {
        jwtTokenInitialValue = jwtToken;
      }
    }

    return new BehaviorSubject<string | null>(jwtTokenInitialValue);
  }
}
