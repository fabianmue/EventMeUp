import { Component } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
})
export class WelcomeComponent {
  loggedIn$: Observable<boolean>;
  username$: Observable<string | null>;

  constructor(readonly authenticationService: AuthenticationService) {
    this.loggedIn$ = authenticationService.loggedIn;
    this.username$ = authenticationService.username;
  }
}
