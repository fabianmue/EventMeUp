import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

import { EventFormComponent } from './components/event-form/event-form.component';
import { EventPreviewComponent } from './components/event-preview/event-preview.component';
import { StartEndPipe } from './pipes/start-end.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatIconModule,
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
