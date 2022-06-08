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
    return this.getStatusCount(SignUpStatus.Accepted);
  }

  get tentative(): number {
    return this.getStatusCount(SignUpStatus.Tentative);
  }

  get declined(): number {
    return this.getStatusCount(SignUpStatus.Declined);
  }

  private getStatusCount(status: SignUpStatus): number {
    if (!this.event?.signUps) {
      return 0;
    }

    return this.event.signUps.filter((signUp) => signUp.status === status)
      .length;
  }
}
