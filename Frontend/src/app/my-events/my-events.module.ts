import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { MyEventsComponent } from './my-events.component';

@NgModule({
  imports: [SharedModule],
  declarations: [MyEventsComponent],
})
export class MyEventsModule {}
