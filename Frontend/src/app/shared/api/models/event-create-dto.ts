/* tslint:disable */
/* eslint-disable */
import { EventCategory } from './event-category';
import { SignUpCreateDto } from './sign-up-create-dto';
export interface EventCreateDto {
  category?: EventCategory;
  description?: null | string;
  end?: null | string;
  location?: null | string;
  notes?: null | string;
  signUps?: null | Array<SignUpCreateDto>;
  start?: string;
  title?: null | string;
}
