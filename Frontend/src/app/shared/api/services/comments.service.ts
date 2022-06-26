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

import { CommentCreateDto } from '../models/comment-create-dto';
import { CommentDto } from '../models/comment-dto';
import { CommentUpdateDto } from '../models/comment-update-dto';

@Injectable({
  providedIn: 'root',
})
export class CommentsService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation commentsCreateComment
   */
  static readonly CommentsCreateCommentPath = '/Events/{eventId}/Signups/{signupId}/Comments';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `commentsCreateComment()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  commentsCreateComment$Response(params: {
    eventId: string;
    signupId: string;
    body?: CommentCreateDto
  }): Observable<StrictHttpResponse<CommentDto>> {

    const rb = new RequestBuilder(this.rootUrl, CommentsService.CommentsCreateCommentPath, 'post');
    if (params) {
      rb.path('eventId', params.eventId, {});
      rb.path('signupId', params.signupId, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<CommentDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `commentsCreateComment$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  commentsCreateComment(params: {
    eventId: string;
    signupId: string;
    body?: CommentCreateDto
  }): Observable<CommentDto> {

    return this.commentsCreateComment$Response(params).pipe(
      map((r: StrictHttpResponse<CommentDto>) => r.body as CommentDto)
    );
  }

  /**
   * Path part for operation commentsUpdateComment
   */
  static readonly CommentsUpdateCommentPath = '/Events/{eventId}/Signups/{signupId}/Comments/{commentId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `commentsUpdateComment()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  commentsUpdateComment$Response(params: {
    signupId: string;
    commentId: string;
    editToken?: string;
    eventId: string;
    body?: CommentUpdateDto
  }): Observable<StrictHttpResponse<CommentDto>> {

    const rb = new RequestBuilder(this.rootUrl, CommentsService.CommentsUpdateCommentPath, 'put');
    if (params) {
      rb.path('signupId', params.signupId, {});
      rb.path('commentId', params.commentId, {});
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
        return r as StrictHttpResponse<CommentDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `commentsUpdateComment$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  commentsUpdateComment(params: {
    signupId: string;
    commentId: string;
    editToken?: string;
    eventId: string;
    body?: CommentUpdateDto
  }): Observable<CommentDto> {

    return this.commentsUpdateComment$Response(params).pipe(
      map((r: StrictHttpResponse<CommentDto>) => r.body as CommentDto)
    );
  }

  /**
   * Path part for operation commentsDeleteSignup
   */
  static readonly CommentsDeleteSignupPath = '/Events/{eventId}/Signups/{signupId}/Comments/{commentId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `commentsDeleteSignup()` instead.
   *
   * This method doesn't expect any request body.
   */
  commentsDeleteSignup$Response(params: {
    signupId: string;
    commentId: string;
    editToken?: string;
    eventId: string;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CommentsService.CommentsDeleteSignupPath, 'delete');
    if (params) {
      rb.path('signupId', params.signupId, {});
      rb.path('commentId', params.commentId, {});
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
   * To access the full response (for headers, for example), `commentsDeleteSignup$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  commentsDeleteSignup(params: {
    signupId: string;
    commentId: string;
    editToken?: string;
    eventId: string;
  }): Observable<void> {

    return this.commentsDeleteSignup$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
