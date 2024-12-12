import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Organization } from '../../_models/organization';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  registerOrganization(model: Organization): Observable<Organization> {
    return this.http.post<Organization>(`${this.baseUrl}organization/register`, model)
  }
}
