import { Observable } from 'rxjs';
export interface AuthenticationService {
  login():void;
  handleCallback(code: string): Observable<any>;
}
