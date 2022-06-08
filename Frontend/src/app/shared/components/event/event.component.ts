import { Component, Input } from '@angular/core';

import { EventDto } from '../../api/models';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss'],
})
export class EventComponent {
  @Input() event!: EventDto;
}
