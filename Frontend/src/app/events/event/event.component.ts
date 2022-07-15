import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import {
  BehaviorSubject,
  first,
  map,
  Observable,
  of,
  Subject,
  switchMap,
  tap,
} from 'rxjs';

import { EventDto, EventUpdateDto } from '../../shared/api/models';
import { EventsService } from '../../shared/api/services';
import { EnterEditTokenDialog } from '../enter-edit-token-dialog/enter-edit-token-dialog';

@Component({
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss'],
})
export class EventComponent implements OnInit {
  event$ = new Subject<EventDto | null>();
  eventEditToken: string | null = null;
  signupEditToken: string | null = null;
  eventFormValid: boolean = false;
  private edit = new BehaviorSubject<boolean>(false);
  private eventId: string | null = null;

  constructor(
    private readonly dialog: MatDialog,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    readonly eventsService: EventsService
  ) {
    this.route.paramMap
      .pipe(
        tap((paramMap) => (this.eventId = paramMap.get('id'))),
        switchMap(() => this.loadEvent())
      )
      .subscribe();
  }

  ngOnInit(): void {
    this.route.queryParamMap
      .pipe(
        first(),
        map((paramMap) => {
          this.eventEditToken = paramMap.get('eventEditToken');
          this.signupEditToken = paramMap.get('signupEditToken');
        }),
        tap(() => this.router.navigate(['.'], { relativeTo: this.route }))
      )
      .subscribe();
  }

  get edit$(): Observable<boolean> {
    return this.edit.asObservable();
  }

  get editValue(): boolean {
    return this.edit.value;
  }

  enableEdit(): void {
    if (this.eventEditToken !== null) {
      this.edit.next(true);
      return;
    }

    this.openDialog().subscribe((editToken) => {
      this.eventEditToken = editToken;
      if (this.eventEditToken !== null) {
        this.edit.next(true);
      }
    });
  }

  disableEdit(): void {
    this.edit.next(false);
  }

  submitEvent(eventId: string, eventUpdateDto: EventUpdateDto): void {
    this.eventsService
      .eventsUpdateEvent({
        eventId: eventId,
        body: eventUpdateDto,
        editToken: this.eventEditToken!,
      })
      .subscribe();
  }

  deleteEvent(event: EventDto): void {
    this.eventsService
      .eventsDeleteEvent({
        eventId: event.id!,
        editToken: this.eventEditToken!,
      })
      .subscribe(() => this.router.navigate(['events']));
  }

  cancelEdit(): void {
    this.edit.next(false);
    this.loadEvent().subscribe();
  }

  private openDialog(): Observable<any> {
    return this.dialog.open(EnterEditTokenDialog).afterClosed();
  }

  private loadEvent(): Observable<EventDto | null> {
    return this.eventId === null
      ? of(null)
      : this.eventsService
          .eventsGetEventById({ eventId: this.eventId })
          .pipe(tap((event) => this.event$.next(event)));
  }
}
