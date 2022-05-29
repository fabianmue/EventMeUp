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

import { EventDto } from '../models/event-dto';

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
   * Path part for operation eventsGetAllEvents
   */
  static readonly EventsGetAllEventsPath = '/Events';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetAllEvents()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllEvents$Response(params?: {
  }): Observable<StrictHttpResponse<Array<EventDto>>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetAllEventsPath, 'get');
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
   * To access the full response (for headers, for example), `eventsGetAllEvents$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetAllEvents(params?: {
  }): Observable<Array<EventDto>> {

    return this.eventsGetAllEvents$Response(params).pipe(
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
    body?: EventDto
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
    body?: EventDto
  }): Observable<EventDto> {

    return this.eventsCreateEvent$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

  /**
   * Path part for operation eventsGetEvent
   */
  static readonly EventsGetEventPath = '/Events/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `eventsGetEvent()` instead.
   *
   * This method doesn't expect any request body.
   */
  eventsGetEvent$Response(params: {
    id: string;
  }): Observable<StrictHttpResponse<EventDto>> {

    const rb = new RequestBuilder(this.rootUrl, EventsService.EventsGetEventPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
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
    id: string;
  }): Observable<EventDto> {

    return this.eventsGetEvent$Response(params).pipe(
      map((r: StrictHttpResponse<EventDto>) => r.body as EventDto)
    );
  }

}
