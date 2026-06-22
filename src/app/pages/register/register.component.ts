import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  name = '';
  email = '';
  password = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  register() {

    const payload = {

      name: this.name,
      email: this.email,
      password: this.password

    };

    this.authService
      .register(payload)
      .subscribe({

        next: () => {

          alert(
            'Registration Successful'
          );

          this.router.navigate(
            ['/login']
          );
        },

        error: () => {

          alert(
            'Registration Failed'
          );
        }

      });
  }
}