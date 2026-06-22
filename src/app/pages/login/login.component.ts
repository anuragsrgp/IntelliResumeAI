import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  email = '';
  password = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login() {

    const payload = {
      email: this.email,
      password: this.password
    };

    this.authService
      .login(payload)
      .subscribe({

        next: (res: any) => {

          localStorage.setItem(
            'token',
            res.token
          );

          localStorage.setItem(
            'name',
            res.name
          );

          localStorage.setItem(
            'email',
            res.email
          );

          alert('Login Successful');

          this.router.navigate([
            '/dashboard'
          ]);

        },

        error: () => {

          alert(
            'Invalid Credentials'
          );

        }

      });

  }

}