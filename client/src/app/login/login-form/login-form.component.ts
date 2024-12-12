import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { NgIf } from '@angular/common';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { AuthFacebookService } from '../../_services/auth/auth-facebook.service';
import { AuthGoogleService } from '../../_services/auth/auth-google.service';
import { TokenStorageService } from '../../_services/token-storage.service';

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
    const token = this.route.snapshot.queryParamMap.get('token');
    const loginService = this.route.snapshot.queryParamMap.get('loginService');
    if (token && loginService) {
      this.tokenStorageService.storeToken(token!);
      this.router.navigate(['/dashboard']);
    }
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
