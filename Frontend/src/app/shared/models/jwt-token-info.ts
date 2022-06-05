export interface JwtTokenInfo {
  raw: string;
  validTo: number;
  username: string;
  email: string;
}
