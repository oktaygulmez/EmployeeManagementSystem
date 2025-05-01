import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersClient, ResetPasswordTokenCommand, ResetPasswordCommand } from '../../services/web-api-client';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})

export class ResetPasswordComponent implements OnInit {

  resetMode: boolean = false;  //hangi formun gösterileceğini belirliyorum false ise e mail girme formu true ise yeni şifre belirleme formu

  hide = true; //password içindeki göz iconu için lazım
  hide2 = true;

  email: string = '';
  password: string = '';
  passwordRepait: string = '';
  token: string = '';

  constructor(private route: ActivatedRoute, private router: Router, private snackbarService: SnackbarService, private userService: UsersClient) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.token = params['token'];
      this.email = params['email'];
      console.log(this.token);
      // Burada token ve email'i kullanabilirsiniz.
      if (this.token && this.email !== undefined) {
        this.resetMode = true;
      } else {
        this.resetMode = false;
      }
    });

  }

  mailCommand: ResetPasswordTokenCommand = new ResetPasswordTokenCommand;
  passwordResetMail(): void {
    if (this.email) {
      this.mailCommand.email = this.email;
      console.log("token : " + this.token);
      console.log("email : " + this.email);
      this.userService.resetPasswordToken(this.mailCommand).subscribe(
        response => {
          console.log(response);
          this.snackbarService.showError("Şifre Hatırlatma Maili Gönderildi.");
        },
        error => {
          console.log(error);
          this.snackbarService.showError("Hatırlatma maili gönderme sırasında hata meydana geldi.");
        }
      );
    } else {
      this.snackbarService.showError("Lütfen E Mail giriniz.");
    }
  }

  resetPasswordCommand: ResetPasswordCommand = new ResetPasswordCommand;
  resetPassword(): void {
    if (this.password && this.passwordRepait) {
      if (this.password == this.passwordRepait) {

        this.resetPasswordCommand.token = this.token;
        this.resetPasswordCommand.email = this.email;
        this.resetPasswordCommand.newPassword = this.password;
        this.userService.resetPassword(this.resetPasswordCommand).subscribe(
          response => {        
            setTimeout(() => {
              this.router.navigate(['/login']);
            }, 1000); // 1000 milisaniye = 1 saniye
            this.snackbarService.showError("Şifre başarıyla güncellendi. Giriş sayfasına yönlendiriliyorsunuz.");
          },
          error => {
            this.snackbarService.showError("Hata.");
          }
        );
      }
      else {
        this.snackbarService.showError("Girdiğiniz şifreler birbiriyle uyuşmuyor.");
      }
    }
  }


}
