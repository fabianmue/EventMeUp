/* tslint:disable */
/* eslint-disable */
import { CommentDto } from './comment-dto';
import { SignupStatus } from './signup-status';
export interface SignupDto {
  comments?: null | Array<CommentDto>;
  createdAt?: string;
  createdBy?: null | string;
  id?: null | string;
  status?: SignupStatus;
}
