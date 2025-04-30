import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtTokenService } from './jwt-token.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
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

  //  isUserInRole(): string {
  //    return this.userRole;
  //  }

  // isUserInRole(): string {
  //   const token = localStorage.getItem('token');

  //   if (token) {
  //     const decodedToken = this.jwtHelper.decodeToken(token);
  //     const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  //     return userRole;
  //   }

  //   return null; // Token yoksa veya decode edilemezse null dönebilirsiniz veya başka bir uygun değer.
  // }

  // isUserInName(): string {
  //   const token = localStorage.getItem('token');

  //   if (token) {
  //     const decodedToken = this.jwtHelper.decodeToken(token);
  //     const userName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  //     return userName;
  //   }

  //   return null; // Token yoksa veya decode edilemezse null dönebilirsiniz veya başka bir uygun değer.
  // }

  // isUserInId(): string {
  //   const token = localStorage.getItem('token');

  //   if (token) {
  //     const decodedToken = this.jwtHelper.decodeToken(token);
  //     const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  //     return userId;
  //   }

  //   return null; // Token yoksa veya decode edilemezse null dönebilirsiniz veya başka bir uygun değer.
  // }
}
