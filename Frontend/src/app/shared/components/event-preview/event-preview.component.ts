import { Component, Input, ViewEncapsulation } from '@angular/core';

import { EventDto } from '../../api/models';

@Component({
  selector: 'app-event-preview',
  templateUrl: './event-preview.component.html',
  styleUrls: ['./event-preview.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventPreviewComponent {
  @Input() event!: EventDto;
}
