/* tslint:disable */
/* eslint-disable */
import { SignUpStatus } from './sign-up-status';
export interface SignUpDto {
  alsoKnownAs?: null | string;
  createdAt?: string;
  id?: number;
  status?: SignUpStatus;
  username?: null | string;
}
