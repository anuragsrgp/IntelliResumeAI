import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders
} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ResumeService {

  private apiUrl =
    'http://localhost:5045/api';

  constructor(
    private http: HttpClient
  ) { }

  uploadResume(file: File) {

    const formData =
      new FormData();

    formData.append(
      'file',
      file
    );

    const token =
      localStorage.getItem(
        'token'
      );

    const headers =
      new HttpHeaders({
        Authorization:
          `Bearer ${token}`
      });

    return this.http.post<any>(
      `${this.apiUrl}/Resume/upload`,
      formData,
      {
        headers
      }
    );
  }

  analyzeResume(
    resumeText: string
  ) {

    const token =
      localStorage.getItem(
        'token'
      );

    const headers =
      new HttpHeaders({
        Authorization:
          `Bearer ${token}`
      });

    return this.http.post<any>(
      `${this.apiUrl}/AI/analyze`,
      {
        resumeText
      },
      {
        headers
      }
    );
  }
}