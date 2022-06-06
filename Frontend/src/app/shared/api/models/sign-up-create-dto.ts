/* tslint:disable */
/* eslint-disable */
import { SignUpStatus } from './sign-up-status';
export interface SignUpCreateDto {
  alsoKnownAs?: null | string;
  email?: null | string;
  status?: SignUpStatus;
  username?: null | string;
}
