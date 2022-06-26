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

import { SignupCreateDto } from '../models/signup-create-dto';
import { SignupDto } from '../models/signup-dto';
import { SignupUpdateDto } from '../models/signup-update-dto';

@Injectable({
  providedIn: 'root',
})
export class SignupsService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation signupsCreateSignup
   */
  static readonly SignupsCreateSignupPath = '/events/{eventId}/Signups';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `signupsCreateSignup()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  signupsCreateSignup$Response(params: {
    eventId: string;
    body?: SignupCreateDto
  }): Observable<StrictHttpResponse<SignupDto>> {

    const rb = new RequestBuilder(this.rootUrl, SignupsService.SignupsCreateSignupPath, 'post');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<SignupDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `signupsCreateSignup$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  signupsCreateSignup(params: {
    eventId: string;
    body?: SignupCreateDto
  }): Observable<SignupDto> {

    return this.signupsCreateSignup$Response(params).pipe(
      map((r: StrictHttpResponse<SignupDto>) => r.body as SignupDto)
    );
  }

  /**
   * Path part for operation signupsUpdateSignup
   */
  static readonly SignupsUpdateSignupPath = '/events/{eventId}/Signups/{signupId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `signupsUpdateSignup()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  signupsUpdateSignup$Response(params: {
    signupId: string;
    editToken?: string;
    eventId: string;
    body?: SignupUpdateDto
  }): Observable<StrictHttpResponse<SignupDto>> {

    const rb = new RequestBuilder(this.rootUrl, SignupsService.SignupsUpdateSignupPath, 'put');
    if (params) {
      rb.path('signupId', params.signupId, {});
      rb.query('editToken', params.editToken, {});
      rb.path('eventId', params.eventId, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<SignupDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `signupsUpdateSignup$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  signupsUpdateSignup(params: {
    signupId: string;
    editToken?: string;
    eventId: string;
    body?: SignupUpdateDto
  }): Observable<SignupDto> {

    return this.signupsUpdateSignup$Response(params).pipe(
      map((r: StrictHttpResponse<SignupDto>) => r.body as SignupDto)
    );
  }

  /**
   * Path part for operation signupsDeleteSignup
   */
  static readonly SignupsDeleteSignupPath = '/events/{eventId}/Signups/{signupId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `signupsDeleteSignup()` instead.
   *
   * This method doesn't expect any request body.
   */
  signupsDeleteSignup$Response(params: {
    signupId: string;
    editToken?: string;
    eventId: string;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, SignupsService.SignupsDeleteSignupPath, 'delete');
    if (params) {
      rb.path('signupId', params.signupId, {});
      rb.query('editToken', params.editToken, {});
      rb.path('eventId', params.eventId, {});
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
   * To access the full response (for headers, for example), `signupsDeleteSignup$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  signupsDeleteSignup(params: {
    signupId: string;
    editToken?: string;
    eventId: string;
  }): Observable<void> {

    return this.signupsDeleteSignup$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
