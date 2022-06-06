/* tslint:disable */
/* eslint-disable */
import { SignUpCreateDto } from './sign-up-create-dto';
export interface EventCreateDto {
  description?: null | string;
  end?: null | string;
  location?: null | string;
  notes?: null | string;
  signUps?: null | Array<SignUpCreateDto>;
  start?: string;
  title?: null | string;
}
