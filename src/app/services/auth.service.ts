import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5045/api/Auth';

  constructor(private http: HttpClient) {}

  login(data: any) {

    return this.http.post(
      `${this.apiUrl}/login`,
      data
    );
  }

  register(data: any) {

    return this.http.post(
      `${this.apiUrl}/register`,
      data
    );
  }

  logout() {

    localStorage.clear();
  }

  isLoggedIn(): boolean {

    return !!localStorage.getItem('token');
  }
}