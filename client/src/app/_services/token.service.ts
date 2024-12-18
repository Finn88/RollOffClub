import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  getToken(): string | null {
    return document.cookie.split('; ').find(row => row.startsWith('AuthToken=')) ?? null;
  }

  isAuthenticated(): boolean {
    const token = document.cookie.split('; ').find(row => row.startsWith('AuthToken='));
    return !!token;
  }
}
