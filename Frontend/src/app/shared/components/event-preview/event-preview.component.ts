import { Component, Input, ViewEncapsulation } from '@angular/core';

import { EventDto, SignupStatus } from '../../api/models';

@Component({
  selector: 'app-event-preview',
  templateUrl: './event-preview.component.html',
  styleUrls: ['./event-preview.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventPreviewComponent {
  @Input() event!: EventDto;

  get accepted(): number {
    return this.getStatusCount(SignupStatus.Accepted);
  }

  get tentative(): number {
    return this.getStatusCount(SignupStatus.Tentative);
  }

  get declined(): number {
    return this.getStatusCount(SignupStatus.Declined);
  }

  private getStatusCount(status: SignupStatus): number {
    if (!this.event?.signups) {
      return 0;
    }

    return this.event.signups.filter((signup) => signup.status === status)
      .length;
  }
}
