import { Component } from '@angular/core';

import { EventCreateDto } from '../../shared/api/models';
import { EventsService } from '../../shared/api/services';

@Component({
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.scss'],
})
export class CreateEventComponent {
  constructor(private readonly eventsService: EventsService) {}

  createEvent(event: EventCreateDto): void {
    this.eventsService.eventsCreateEvent({ body: event }).subscribe();
  }
}
