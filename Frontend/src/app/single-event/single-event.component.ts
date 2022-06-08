import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { first, map, Observable, of, switchMap } from 'rxjs';

import { EventDto } from '../shared/api/models';
import { EventsService } from '../shared/api/services';

@Component({
  templateUrl: './single-event.component.html',
  styleUrls: ['./single-event.component.scss'],
})
export class SingleEventComponent implements OnInit {
  event$: Observable<EventDto | null>;

  constructor(
    readonly route: ActivatedRoute,
    readonly eventsService: EventsService
  ) {
    this.event$ = this.route.paramMap.pipe(
      map((paramMap) => paramMap.get('id')),
      switchMap((eventId) => {
        if (eventId === null) {
          return of(null);
        }

        return eventsService.eventsGetEvent({ eventId });
      })
    );
  }

  ngOnInit(): void {}
}
