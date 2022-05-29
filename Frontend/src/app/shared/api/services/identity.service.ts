/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { UserLoginDto } from '../models/user-login-dto';
import { UserLoginResponseDto } from '../models/user-login-response-dto';
import { UserRegisterDto } from '../models/user-register-dto';

@Injectable({
  providedIn: 'root',
})
export class IdentityService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation identityLogin
   */
  static readonly IdentityLoginPath = '/Identity/login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `identityLogin()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  identityLogin$Response(params?: {
    body?: UserLoginDto
  }): Observable<StrictHttpResponse<UserLoginResponseDto>> {

    const rb = new RequestBuilder(this.rootUrl, IdentityService.IdentityLoginPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserLoginResponseDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `identityLogin$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  identityLogin(params?: {
    body?: UserLoginDto
  }): Observable<UserLoginResponseDto> {

    return this.identityLogin$Response(params).pipe(
      map((r: StrictHttpResponse<UserLoginResponseDto>) => r.body as UserLoginResponseDto)
    );
  }

  /**
   * Path part for operation identityRegister
   */
  static readonly IdentityRegisterPath = '/Identity/register';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `identityRegister()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  identityRegister$Response(params?: {
    body?: UserRegisterDto
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, IdentityService.IdentityRegisterPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `identityRegister$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  identityRegister(params?: {
    body?: UserRegisterDto
  }): Observable<void> {

    return this.identityRegister$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
