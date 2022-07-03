import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LocalStorageHelper } from './shared/local-storage-helper';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor() {
    if (!environment.production) {
      ['0', '1', '2'].forEach((eventId) => {
        LocalStorageHelper.addMyEvent(eventId, '');
        LocalStorageHelper.addMySignup(eventId, eventId, '');
      });
    }
  }
}
