import { Component } from '@angular/core';
import { AuthManager } from './helper/authManager';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private authManager: AuthManager) { }

  isLoggedIn() {
    return this.authManager.isLoggedIn();
  }

}
