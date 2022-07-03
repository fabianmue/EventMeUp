import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
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
import { EnterEditTokenDialog } from '../enter-edit-token-dialog/enter-edit-token-dialog';

@Component({
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss'],
})
export class EventComponent implements OnInit {
  event$: Observable<EventDto | null>;
  eventEditToken: string | null = null;
  signupEditToken: string | null = null;
  private edit = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly dialog: MatDialog,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
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

  toggleEdit(): void {
    if (this.editValue) {
      this.edit.next(false);
      return;
    }

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

  deleteEvent(): void {
    console.log('ToDo: implement delete');
  }

  clearEventEditToken(): void {
    this.eventEditToken = null;
    this.edit.next(false);
  }

  private openDialog(): Observable<any> {
    return this.dialog.open(EnterEditTokenDialog).afterClosed();
  }
}
