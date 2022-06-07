import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs';

import { EventDto } from '../shared/api/models';
import { EventsService } from '../shared/api/services';

@Component({
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventsComponent implements OnInit {
  owned$: Observable<Array<EventDto>>;
  signedUp$: Observable<Array<EventDto>>;

  constructor(readonly eventsService: EventsService) {
    this.owned$ = eventsService.eventsGetAllMyOwnedEvents();
    this.signedUp$ = eventsService.eventsGetAllMySignedUpEvents();
  }

  ngOnInit(): void {}
}