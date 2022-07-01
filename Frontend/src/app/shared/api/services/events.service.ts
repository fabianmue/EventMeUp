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
import { EventUpdateDto } from '../models/event-update-dto';

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
   * Path part for operation eventsGetEventById
   */
  static readonly EventsGetEventByIdPath = '/Events/{eventId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetEventById()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetEventById$Response(params: {
    eventId: string;
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetEventByIdPath, 'get');
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
   * To access the full response (for headers, for example), `eventsGetEventById$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetEventById(params: {
    eventId: string;
  }): Observable<EventDto> {

    return this.eventsGetEventById$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * Path part for operation eventsUpdateEvent
   */
  static readonly EventsUpdateEventPath = '/Events/{eventId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsUpdateEvent()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsUpdateEvent$Response(params: {
    eventId: string;
    editToken?: string;
    body?: EventUpdateDto
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsUpdateEventPath, 'put');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.query('editToken', params.editToken, {});
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
   * To access the full response (for headers, for example), `eventsUpdateEvent$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsUpdateEvent(params: {
    eventId: string;
    editToken?: string;
    body?: EventUpdateDto
  }): Observable<EventDto> {

    return this.eventsUpdateEvent$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * Path part for operation eventsDeleteEvent
   */
  static readonly EventsDeleteEventPath = '/Events/{eventId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsDeleteEvent()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsDeleteEvent$Response(params: {
    eventId: string;
    editToken?: string;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsDeleteEventPath, 'delete');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.query('editToken', params.editToken, {});
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
   * To access the full response (for headers, for example), `eventsDeleteEvent$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsDeleteEvent(params: {
    eventId: string;
    editToken?: string;
  }): Observable<void> {

    return this.eventsDeleteEvent$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation eventsGetEventsByIds
   */
  static readonly EventsGetEventsByIdsPath = '/Events/ids';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetEventsByIds()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsGetEventsByIds$Response(params?: {
    body?: Array<string>
  }): Observable<StrictHttpResponse<Array<EventDto>>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetEventsByIdsPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
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
   * To access the full response (for headers, for example), `eventsGetEventsByIds$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  eventsGetEventsByIds(params?: {
    body?: Array<string>
  }): Observable<Array<EventDto>> {

    return this.eventsGetEventsByIds$Response(params).pipe(
      map((r: StrictHttpResponse<Array<EventDto>>) => r.body as Array<EventDto>)
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
