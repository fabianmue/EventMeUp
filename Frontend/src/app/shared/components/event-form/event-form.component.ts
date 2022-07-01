import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { EventCreateDto, EventDto } from '../../api/models';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss'],
})
export class EventFormComponent {
  @Input() event?: EventDto;
  @Input() set edit(edit: boolean) {
    const toggle = edit
      ? () => this.eventForm.enable()
      : () => this.eventForm.disable();
    toggle();
  }
  @Output() submit = new EventEmitter<EventDto | EventCreateDto>();
  eventForm: FormGroup;

  constructor(readonly formBuilder: FormBuilder) {
    this.eventForm = this.formBuilder.group({
      title: [''],
    });
  }

  submitForm(): void {
    if (this.event) {
      const eventDto = {} as EventDto;
      this.submit.emit(eventDto);
      return;
    }

    const eventCreateDto = {} as EventCreateDto;
    this.submit.emit(eventCreateDto);
  }
}
