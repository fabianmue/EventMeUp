import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';

import { SharedModule } from '../shared/shared.module';
import { EventsComponent } from './events.component';

const routes: Routes = [{ path: '', component: EventsComponent }];

@NgModule({
  imports: [SharedModule, RouterModule.forChild(routes), MatTabsModule],
  declarations: [EventsComponent],
})
export class MyEventsModule {}
