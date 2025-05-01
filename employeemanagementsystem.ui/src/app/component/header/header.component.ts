import { Component } from '@angular/core';
import { AuthManager } from '../../helper/authManager';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent {

  userName: string;
  constructor(private authManager: AuthManager) {
    // Retrieve values from localStorage
    this.userName = localStorage.getItem('userName') ?? ''; 
  }

  logout() {
    this.authManager.logout();
  }

}
