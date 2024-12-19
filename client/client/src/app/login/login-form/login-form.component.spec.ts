import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginFormComponent, customEmailValidator } from './login-form.component';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { DebugElement } from '@angular/core';
import { of } from 'rxjs';

describe('LoginFormComponent', () => {
  let component: LoginFormComponent;
  let fixture: ComponentFixture<LoginFormComponent>;
  let emailInput: DebugElement;
  let passwordInput: DebugElement;
  let form: NgForm;
  let emailModel: NgModel;
  let passwordModel: NgModel;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginFormComponent, FormsModule, FormsModule, RouterLink, PopoverModule],
      providers: [
        {
          provide: ActivatedRoute,
           useValue: {
            snapshot: { paramMap: of({ get: () => 'mockValue' }) },
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    emailInput = fixture.debugElement.query(By.css('input[type="email"]'));
    emailModel = fixture.debugElement.query(By.css('input[type="email"]')).injector.get(NgModel);

    passwordInput = fixture.debugElement.query(By.css('input[type="password"]'));
    passwordModel = fixture.debugElement.query(By.css('input[type="password"]')).injector.get(NgModel);

    form = fixture.debugElement.query(By.css('form')).injector.get(NgForm);
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should show an error message when email is invalid', () => {
    emailInput.nativeElement.value = 'invalid email';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    
    component.onEmailInput(emailModel);
    expect(component.emailErrorVisible).toBeFalse();
  });

  it('should show an error message when email is invalid and submitted', () => {
    emailInput.nativeElement.value = 'invalid email';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onEmailInput(emailModel);
    component.onSubmitLoginForm(form);
    expect(component.emailErrorVisible).toBeTrue();
  });

  it('should hide email error message when email becomes valid', () => {
    emailInput.nativeElement.value = 'valid@example.com';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onEmailInput(emailModel);
    expect(component.emailErrorVisible).toBeFalse();
  });

  it('should show an error message when password is invalid', () => {
    passwordInput.nativeElement.value = '';
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onPasswordInput(passwordModel);
    expect(component.passwordErrorVisible).toBeFalse();
  });

  it('should show an error message when password is invalid and submitted', () => {
    passwordInput.nativeElement.value = '';
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onPasswordInput(passwordModel);
    component.onSubmitLoginForm(form);
    expect(component.passwordErrorVisible).toBeFalse();
  });


  it('should submit the form if both email and password are valid', () => {
    emailInput.nativeElement.value = 'valid@example.com';
    passwordInput.nativeElement.value = 'validPassword';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onSubmitLoginForm(form);
    expect(component.emailErrorVisible).toBeFalse();
    expect(component.passwordErrorVisible).toBeFalse();
  });

  it('should show email error when form is submitted with invalid email', () => {
    emailInput.nativeElement.value = 'invalid-email';
    passwordInput.nativeElement.value = 'validPassword';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onSubmitLoginForm(form);
    expect(component.emailErrorVisible).toBeTrue();
    expect(component.passwordErrorVisible).toBeFalse();
  });

  it('should show password error when form is submitted with invalid password', () => {
    emailInput.nativeElement.value = 'valid@example.com';
    passwordInput.nativeElement.value = '';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onSubmitLoginForm(form);
    expect(component.emailErrorVisible).toBeFalse();
    expect(component.passwordErrorVisible).toBeTrue();
  });

  it('should show password error when form is submitted with invalid email and password', () => {
    emailInput.nativeElement.value = 'valid@example';
    passwordInput.nativeElement.value = '';
    emailInput.nativeElement.dispatchEvent(new Event('input'));
    passwordInput.nativeElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    component.onSubmitLoginForm(form);
    expect(component.emailErrorVisible).toBeTrue();
    expect(component.passwordErrorVisible).toBeFalse();
  });
});
