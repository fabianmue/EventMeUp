import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

import { EventComponent } from './components/event/event.component';
import { EventPreviewComponent } from './components/event-preview/event-preview.component';
import { StartEndPipe } from './pipes/start-end.pipe';

@NgModule({
  imports: [CommonModule, MatCardModule, MatIconModule],
  declarations: [StartEndPipe, EventComponent, EventPreviewComponent],
  providers: [StartEndPipe],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    EventComponent,
    EventPreviewComponent,
  ],
})
export class SharedModule {}
