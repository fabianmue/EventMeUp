import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, filter, Subscription } from 'rxjs';

import {
  EventCategory,
  EventCreateDto,
  EventDto,
  EventUpdateDto,
} from '../../api/models';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EventFormComponent implements OnInit, OnDestroy {
  @Input() set event(event: EventDto | undefined) {
    if (event) {
      this.updateForm(event);
    }
  }
  @Input() set edit(edit: boolean) {
    const toggle = edit
      ? () => this.eventForm.enable()
      : () => {
          this.eventForm.disable();
          if (this.eventForm.touched) {
            this.submitForm();
          }
        };
    toggle();
  }
  @Output() submit = new EventEmitter<EventUpdateDto | EventCreateDto>();
  @Output() valid = new BehaviorSubject<boolean>(false);
  eventForm: FormGroup;
  categories: Array<{ value: string | null; viewValue: string }> = [
    { value: null, viewValue: '(none)' },
    ...Object.values(EventCategory).map((eventCategory) => ({
      value: eventCategory,
      viewValue: eventCategory,
    })),
  ];
  private eventFormValueChangeSubscription?: Subscription;
  private eventFormUpdated = false;

  constructor() {
    this.eventForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      start: new FormControl(undefined, [Validators.required]),
      end: new FormControl(undefined),
      location: new FormControl(''),
      category: new FormControl(undefined),
    });
  }

  ngOnInit(): void {
    this.eventFormValueChangeSubscription = this.eventForm.valueChanges
      .pipe(filter(() => this.eventForm.valid !== this.valid.value))
      .subscribe(() => this.valid.next(this.eventForm.valid));
  }

  ngOnDestroy(): void {
    this.eventFormValueChangeSubscription?.unsubscribe();
  }

  updateForm(event: EventDto): void {
    this.eventForm.get('title')?.setValue(event.title);
    this.eventForm.get('description')?.setValue(event.description);
    this.eventForm.get('start')?.setValue(event.start);
    this.eventForm.get('end')?.setValue(event.end);
    this.eventForm.get('location')?.setValue(event.location);
    this.eventForm.get('category')?.setValue(event.category);
    this.eventFormUpdated = true;
  }

  submitForm(): void {
    if (this.eventFormUpdated) {
      const eventDto = {
        title: this.eventForm.get('title')?.value,
        description: this.eventForm.get('description')?.value,
        start: this.eventForm.get('start')?.value,
        end: this.eventForm.get('end')?.value,
        location: this.eventForm.get('location')?.value,
        category: this.eventForm.get('category')?.value,
      } as EventUpdateDto;
      this.submit.emit(eventDto);
      return;
    }

    const eventCreateDto = {} as EventCreateDto;
    this.submit.emit(eventCreateDto);
  }
}
