import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { ApiModule } from './shared/api/api.module';
import { JwtTokenHelper } from './shared/helpers/jwt-token-helper';
import { HeaderModule } from './header/header.module';
import { AppComponent } from './app.component';
import { environment } from 'src/environments/environment';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    ApiModule.forRoot({ rootUrl: environment.webapiRooturl }),
    JwtModule.forRoot({
      config: {
        tokenGetter: JwtTokenHelper.getJwtToken,
        allowedDomains: ['localhost:5000'],
      },
    }),
    SharedModule,
    HeaderModule,
  ],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
