import { CanActivateFn } from '@angular/router';

import { AuthenticationService } from './services/authentication.service';

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {

    if (!this.authService.isLoggedIn()) {
      return this.router.createUrlTree(['/login']);
    }

    const requiredRole = next.data['requiredRole']; // İstenilen rolü alma
    const userRole = this.authService.isUserInRole();

    if (requiredRole && userRole !== requiredRole) {
      // Kullanıcının rolü istenen role uymuyorsa giriş izni verme
      return this.router.createUrlTree(['/UnAuthorized']);
    }

    return true; // Giriş izni ver
  }

}
