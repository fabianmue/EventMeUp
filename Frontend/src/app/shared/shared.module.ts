import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ApiModule } from './api/api.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ApiModule.forRoot({ rootUrl: environment.webapiRooturl }),
  ],
  exports: [CommonModule, FormsModule, ReactiveFormsModule, ApiModule],
})
export class SharedModule {}
