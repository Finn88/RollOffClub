import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  baseUrl = environment.apiUrl;

  logout(): void {
    this.http.get(`${this.baseUrl}auth/logout`).subscribe({
      next: _ => this.router.navigate([''])
    });
  }
}
