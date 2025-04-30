import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  userName!: string;
  password!: string;
  rememberMe = true;
  hide = true;

  constructor(
    private toastr: ToastrService,
    private router: Router,
    private userService: EmployeeService
  ) {}

  onLogin(): void {
    if (this.userName && this.password) {
      this.userService.userLogin(this.userName, this.password).subscribe(
        (response) => {
          if (this.rememberMe == true) {
            localStorage.setItem('rememberedUsername', this.userName);
            localStorage.setItem('rememberedPassword', this.password);
            localStorage.setItem('rememberedMe', 'true');
          } else {
            localStorage.setItem('rememberedUsername', '');
            localStorage.setItem('rememberedPassword', '');
            localStorage.setItem('rememberedMe', 'false');
          }
          localStorage.setItem('token', response.token);
          this.toastr.success('Giriş başarılı.');
          this.router.navigate(['/Employee']);
        },
        (error) => {
          this.toastr.error(
            'Giriş başarısız. Kullanıcı adı veya şifre hatalı.'
          );
        }
      );
    } else {
      this.toastr.warning(
        'Giriş yapabilmek için kullanıcı adı ve şifre girmelisiniz.'
      );
    }
  }
}
