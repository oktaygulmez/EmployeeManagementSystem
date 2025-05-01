import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {
  private jwtHelper: JwtHelperService;

  constructor() {
    this.jwtHelper = new JwtHelperService();
  }

  // Tokeni çözme işlemi
  decodeToken(token: string): any {
    return this.jwtHelper.decodeToken(token);
  }

  // Tokenin süresi dolmuş mu?
  isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }
}
