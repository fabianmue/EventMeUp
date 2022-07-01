import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EventsComponent } from './events.component';
import { EventComponent } from './event/event.component';
import { CreateEventComponent } from './create-event/create-event.component';

const routes: Routes = [
  {
    path: '',
    component: EventsComponent,
  },
  {
    path: 'create',
    component: CreateEventComponent,
  },
  {
    path: ':id',
    component: EventComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EventsRoutingModule {}
