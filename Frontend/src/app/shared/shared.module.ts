import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { EventFormComponent } from './components/event-form/event-form.component';
import { EventPreviewComponent } from './components/event-preview/event-preview.component';
import { StartEndPipe } from './pipes/start-end.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatNativeDateModule,
    MatSelectModule,
  ],
  declarations: [StartEndPipe, EventFormComponent, EventPreviewComponent],
  providers: [StartEndPipe],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    EventFormComponent,
    EventPreviewComponent,
  ],
})
export class SharedModule {}
