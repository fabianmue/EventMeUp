import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';

import { SharedModule } from '../shared/shared.module';
import { EventsComponent } from './events.component';
import { EventComponent } from './event/event.component';
import { CreateEventComponent } from './create-event/create-event.component';
import { EventsRoutingModule } from './events-routing.module';

@NgModule({
  imports: [SharedModule, EventsRoutingModule, MatTabsModule],
  declarations: [EventsComponent, EventComponent, CreateEventComponent],
})
export class MyEventsModule {}
