import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(
    private router: Router
  ) {}

  get isLoggedIn() {

    return !!localStorage.getItem(
      'token'
    );
  }

  get userName() {

    return localStorage.getItem(
      'userName'
    ) || 'User';
  }

  logout() {

    localStorage.clear();

    this.router.navigate([
      '/login'
    ]);
  }
}