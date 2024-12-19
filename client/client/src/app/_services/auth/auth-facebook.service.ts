import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../../_models/authenticationService';

@Injectable({
  providedIn: 'root'
})
export class AuthFacebookService implements AuthenticationService {
  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  login(): void {
    window.location.href = `${this.baseUrl}authfacebook/login`; 
  }

  getToken(code: string): Observable<any> {
    return this.http.post(`${this.baseUrl}authfacebook/token`,
      JSON.stringify(code),
      { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }
    );
  }
}
