import { Component, Input, ViewEncapsulation } from '@angular/core';

import { EventDto, SignUpStatus } from '../../api/models';

@Component({
  selector: 'app-event-preview',
  templateUrl: './event-preview.component.html',
  styleUrls: ['./event-preview.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventPreviewComponent {
  @Input() event!: EventDto;

  get accepted(): number {
    if (!this.event?.signUps) {
      return 0;
    }

    return this.event.signUps.filter(
      (signUp) => signUp.status === SignUpStatus.Accepted
    ).length;
  }

  get tentative(): number {
    if (!this.event?.signUps) {
      return 0;
    }

    return this.event.signUps.filter(
      (signUp) => signUp.status === SignUpStatus.Tentative
    ).length;
  }

  get declined(): number {
    if (!this.event?.signUps) {
      return 0;
    }

    return this.event.signUps.filter(
      (signUp) => signUp.status === SignUpStatus.Declined
    ).length;
  }
}
