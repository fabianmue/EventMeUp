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

import { EventCreateDto } from '../models/event-create-dto';
import { EventDto } from '../models/event-dto';
import { SignUpCreateDto } from '../models/sign-up-create-dto';

@Injectable({
  providedIn: 'root',
})
export class EventsService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation eventsGetAllMyOwnedEvents
   */
  static readonly EventsGetAllMyOwnedEventsPath = '/Events/owned';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetAllMyOwnedEvents()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllMyOwnedEvents$Response(params?: {
  }): Observable<StrictHttpResponse<Array<EventDto>>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetAllMyOwnedEventsPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<EventDto>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsGetAllMyOwnedEvents$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllMyOwnedEvents(params?: {
  }): Observable<Array<EventDto>> {

    return this.eventsGetAllMyOwnedEvents$Response(params).pipe(
      map((r: StrictHttpResponse<Array<EventDto>>) => r.body as Array<EventDto>)
    );
  }

  /**
   * Path part for operation eventsGetAllMySignedUpEvents
   */
  static readonly EventsGetAllMySignedUpEventsPath = '/Events/signedup';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetAllMySignedUpEvents()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllMySignedUpEvents$Response(params?: {
  }): Observable<StrictHttpResponse<Array<EventDto>>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetAllMySignedUpEventsPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<EventDto>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsGetAllMySignedUpEvents$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllMySignedUpEvents(params?: {
  }): Observable<Array<EventDto>> {

    return this.eventsGetAllMySignedUpEvents$Response(params).pipe(
      map((r: StrictHttpResponse<Array<EventDto>>) => r.body as Array<EventDto>)
    );
  }

  /**
   * Path part for operation eventsGetEvent
   */
  static readonly EventsGetEventPath = '/Events/{eventId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetEvent()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetEvent$Response(params: {
    eventId: string;
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetEventPath, 'get');
    if (params) {
      rb.path('eventId', params.eventId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<EventDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsGetEvent$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetEvent(params: {
    eventId: string;
  }): Observable<EventDto> {

    return this.eventsGetEvent$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * Path part for operation eventsAddEventSignUp
   */
  static readonly EventsAddEventSignUpPath = '/Events/{eventId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsAddEventSignUp$Plain()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsAddEventSignUp$Plain$Response(params: {
    eventId: string;
    body?: SignUpCreateDto
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsAddEventSignUpPath, 'post');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<EventDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsAddEventSignUp$Plain$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsAddEventSignUp$Plain(params: {
    eventId: string;
    body?: SignUpCreateDto
  }): Observable<EventDto> {

    return this.eventsAddEventSignUp$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsAddEventSignUp$Json()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsAddEventSignUp$Json$Response(params: {
    eventId: string;
    body?: SignUpCreateDto
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsAddEventSignUpPath, 'post');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<EventDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsAddEventSignUp$Json$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsAddEventSignUp$Json(params: {
    eventId: string;
    body?: SignUpCreateDto
  }): Observable<EventDto> {

    return this.eventsAddEventSignUp$Json$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * Path part for operation eventsCreateEvent
   */
  static readonly EventsCreateEventPath = '/Events';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsCreateEvent()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsCreateEvent$Response(params?: {
    body?: EventCreateDto
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsCreateEventPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<EventDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `eventsCreateEvent$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsCreateEvent(params?: {
    body?: EventCreateDto
  }): Observable<EventDto> {

    return this.eventsCreateEvent$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

}
