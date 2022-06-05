import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { MyEventsComponent } from './my-events.component';

const routes: Routes = [{ path: '', component: MyEventsComponent }];

@NgModule({
  imports: [SharedModule, RouterModule.forChild(routes)],
  declarations: [MyEventsComponent],
})
export class MyEventsModule {}
