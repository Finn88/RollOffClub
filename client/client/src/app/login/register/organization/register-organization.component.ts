import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, ChangeDetectionStrategy, Component, DoCheck, OnChanges, OnDestroy, OnInit, SimpleChanges, inject } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { RegisterService } from '../../../_services/register/register.service';
import { NgFor, NgForOf, NgIf } from '@angular/common';
import { from, interval, map, Observable, Subscription, switchMap } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-register-organization',
  templateUrl: './register-organization.component.html',
  styleUrl: './register-organization.component.css',
  imports: [RouterLink, ReactiveFormsModule, NgIf, NgFor, NgForOf],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterOrganizationComponent implements OnInit, OnDestroy
{
  private fb = inject(FormBuilder);
  private registerService = inject(RegisterService)
  private router = inject(Router);


  registerForm: FormGroup = new FormGroup({});
  registerFormSubscribe: Subscription|undefined;
  registerSubmitted = false;
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.initializeForm();
    this.registerFormSubscribe = this.registerForm.valueChanges.subscribe(() => {
      if (this.registerSubmitted) {
        this.getErrorMessage();
      }
    });0
  }
  
  ngOnDestroy(): void {
    this.registerFormSubscribe?.unsubscribe();
  }

  initializeForm(): void {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      city: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  getErrorMessage(): void {
    this.validationErrors = [];
    this.validateControl("name");
    this.validateControl("description");
    this.validateControl("country");
    this.validateControl("city");
  }

  validateControl(name: string): void {
    if (this.registerForm.get(name)?.hasError('required'))
      this.validationErrors.push(`${name.charAt(0).toUpperCase()}${name.slice(1)} is required`);
  }

  submit(): void {
    this.registerSubmitted = true;
    this.getErrorMessage();
    if (!this.registerForm.valid) return;

    this.registerService.register(this.registerForm.value).subscribe({
      next: _ => { this.router.navigateByUrl(''); },
      error: error => this.validationErrors = error
    });
  } 

}

