import { Component, ViewEncapsulation } from '@angular/core';
import { map, Observable, of } from 'rxjs';

import { EventDto, SignupDto } from '../shared/api/models';
import { EventsService } from '../shared/api/services';
import {
  EventAndSignupIdWithEditToken,
  EventIdWithEditToken,
  LocalStorageHelper,
} from '../shared/local-storage-helper';

type EventWithEditToken = { event: EventDto; editToken: string };
type SignupWithEventAndEditToken = {
  event: EventDto;
  signup: SignupDto;
  editToken: string;
};

@Component({
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventsComponent {
  myEvents$: Observable<Array<EventWithEditToken>>;
  mySignups$: Observable<Array<SignupWithEventAndEditToken>>;

  constructor(readonly eventsService: EventsService) {
    this.myEvents$ = this.getMyEvents(LocalStorageHelper.myEventIds);
    this.mySignups$ = this.getMySignups(LocalStorageHelper.mySignupIds);
  }

  private getMyEvents(
    eventIds: Array<EventIdWithEditToken>
  ): Observable<Array<EventWithEditToken>> {
    const idsOnly = eventIds.map((eventIds) => eventIds.eventId);
    return this.eventsService.eventsGetEventsByIds({ body: idsOnly }).pipe(
      map((events) => events.filter((event) => idsOnly.includes(event.id!))),
      map((events) =>
        events.map((event) => {
          const match = eventIds.find(
            (eventId) => eventId.eventId === event.id
          );
          return {
            event,
            editToken: match!.editToken,
          };
        })
      )
    );
  }

  private getMySignups(
    signupIds: Array<EventAndSignupIdWithEditToken>
  ): Observable<Array<SignupWithEventAndEditToken>> {
    const idsOnly = signupIds.map((signupIds) => signupIds.eventId);
    return this.eventsService.eventsGetEventsByIds({ body: idsOnly }).pipe(
      map((events) => events.filter((event) => idsOnly.includes(event.id!))),
      map((events) =>
        events.map((event) => {
          const match = signupIds.find(
            (signupId) => signupId.eventId === event.id
          );
          return {
            event,
            signup: event.signups?.find(
              (signup) => signup.id === match?.signupId
            )!,
            editToken: match!.editToken,
          };
        })
      )
    );
  }
}
