/* tslint:disable */
/* eslint-disable */
import { EventCategory } from './event-category';
export interface EventCreateDto {
  category?: EventCategory;
  createdBy?: null | string;
  description?: null | string;
  end?: null | string;
  location?: null | string;
  start?: string;
  title?: null | string;
}
