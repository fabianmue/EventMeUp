/* tslint:disable */
/* eslint-disable */
import { SignUpDto } from './sign-up-dto';
export interface EventDto {
  createdAt?: string;
  description?: null | string;
  end?: null | string;
  id?: null | string;
  location?: null | string;
  notes?: null | string;
  signUps?: null | Array<SignUpDto>;
  start?: string;
  title?: null | string;
}
