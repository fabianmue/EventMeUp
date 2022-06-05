import { Component } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  loggedIn$: Observable<boolean>;

  constructor(private readonly authenticationService: AuthenticationService) {
    this.loggedIn$ = authenticationService.loggedIn;
  }

  logout(): void {
    this.authenticationService.logout();
  }
}
