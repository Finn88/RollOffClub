import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { NgIf } from '@angular/common';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { AuthFacebookService } from '../../_services/auth/auth-facebook.service';
import { AuthGoogleService } from '../../_services/auth/auth-google.service';
import { TokenStorageService } from '../../_services/token-storage.service';
import { AuthenticationService } from '../../_models/authenticationService';

export function customEmailValidator(control: any) {
  const email = control.value;
  const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  return email && !emailPattern.test(email) ? { invalidEmail: true } : null;
}

@Component({
  standalone: true,
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
  imports: [RouterLink, FormsModule, NgIf, PopoverModule],
})

export class LoginFormComponent implements OnInit {  
  protected showEmailError: boolean = false;
  protected showPasswordError: boolean = false;
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authGoogleService = inject(AuthGoogleService);
  private authFacebookService = inject(AuthFacebookService);
  private tokenStorageService = inject(TokenStorageService);
  user: any = null;

  ngOnInit(): void {
    const code = this.route.snapshot.queryParamMap.get('code');
    const authServiceName = this.route.snapshot.queryParamMap.get('authService');
    if (code && authServiceName) {
      switch (authServiceName){
        case 'facebook':
          this.loadToken(this.authFacebookService, code);
          break;
        case 'google':
          this.loadToken(this.authGoogleService, code);
          break;
        default: return;
      }
    }
  }

  private loadToken(service: AuthenticationService, code: string): void {
    service
      .getToken(code)
      .subscribe({
        next: (data) => {
          this.tokenStorageService.storeToken(data.token!);
          this.router.navigate(['/dashboard']);
        }
      })
  }

  loginGoogle(): void {
    this.authGoogleService.login();
  }

  loginFacebook(): void {
    this.authFacebookService.login();
  }
  
  get emailErrorVisible(): boolean {
    return this.showEmailError;
  }

  get passwordErrorVisible(): boolean {
    return this.showPasswordError;
  }

  onEmailInput(email: NgModel): void {
    if(this.showEmailError && email.valid){
      this.showEmailError = false;
    }
    this.showPasswordError = false;
  }

  onPasswordInput(password: NgModel): void {
    if(this.showPasswordError && password.valid){
      this.showPasswordError = false;
    }
  }

  onSubmitLoginForm(loginForm: NgForm): void {
    if (loginForm.controls['email'].invalid) {
      this.showEmailError = true;
      this.showPasswordError = false;
    }
    else if (loginForm.controls['password'].invalid) {
      this.showPasswordError = true;
    }
  }
}
