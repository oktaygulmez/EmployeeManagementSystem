import { CanActivateFn } from '@angular/router';
import { AuthService } from './services/auth.service';

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

//export const authGuard: CanActivateFn = (route, state) => {
//  return true;

//};

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {

    // if (!this.authService.isLoggedIn()) {
    //   return this.router.createUrlTree(['/login']);
    // }

    return true; // Giriş izni ver
  }

}
