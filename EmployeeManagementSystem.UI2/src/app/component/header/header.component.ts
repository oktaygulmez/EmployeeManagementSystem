import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent {


  userName: string;


  constructor(private authService: AuthService) {
    // Retrieve values from localStorage
    this.userName = localStorage.getItem('userName') ?? ''; 

  }


  logout() {
    this.authService.logout();
  }

}
