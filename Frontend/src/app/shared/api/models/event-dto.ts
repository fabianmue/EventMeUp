/* tslint:disable */
/* eslint-disable */
import { EventCategory } from './event-category';
import { SignupDto } from './signup-dto';
export interface EventDto {
  category?: EventCategory;
  createdAt?: string;
  createdBy?: null | string;
  description?: null | string;
  end?: null | string;
  id?: null | string;
  location?: null | string;
  signups?: null | Array<SignupDto>;
  start?: string;
  title?: null | string;
}
