import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  BehaviorSubject,
  first,
  map,
  Observable,
  of,
  switchMap,
  tap,
} from 'rxjs';

import { EventDto } from '../../shared/api/models';
import { EventsService } from '../../shared/api/services';

@Component({
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss'],
})
export class EventComponent implements OnInit {
  event$: Observable<EventDto | null>;
  private edit = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly route: ActivatedRoute,
    readonly eventsService: EventsService
  ) {
    this.event$ = this.route.paramMap.pipe(
      map((paramMap) => paramMap.get('id')),
      switchMap((eventId) =>
        eventId === null
          ? of(null)
          : eventsService.eventsGetEventById({ eventId })
      )
    );
  }

  ngOnInit(): void {
    this.route.queryParamMap
      .pipe(
        map((paramMap) => {
          const edit = paramMap.get('edit');
          if (edit === null) {
            return false;
          }

          return Boolean(edit);
        }),
        first(),
        tap((edit) => this.edit.next(edit))
      )
      .subscribe();
  }

  get edit$(): Observable<boolean> {
    return this.edit.asObservable();
  }

  toggleEdit(): void {
    this.edit.next(!this.edit.value);
  }
}
