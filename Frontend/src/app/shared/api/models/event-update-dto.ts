/* tslint:disable */
/* eslint-disable */
import { EventCategory } from './event-category';
export interface EventUpdateDto {
  category?: EventCategory;
  description?: null | string;
  end?: null | string;
  location?: null | string;
  start?: string;
  title?: null | string;
}
