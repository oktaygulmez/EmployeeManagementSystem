import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtTokenService } from './jwt-token.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  constructor(private jwtService: JwtTokenService, private jwtHelper: JwtHelperService, private router: Router) { }

  logout(): void {
    // Tokeni sil
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    // Token var mı ve süresi dolmamış mı kontrol et
    const token = localStorage.getItem('token');
    return !!token && !this.jwtService.isTokenExpired(token);
  }

}
