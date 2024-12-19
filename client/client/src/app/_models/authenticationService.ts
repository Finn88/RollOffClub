import { Observable } from 'rxjs';
export interface AuthenticationService {
  login():void;
  getToken(code: string): Observable<any>;
}
