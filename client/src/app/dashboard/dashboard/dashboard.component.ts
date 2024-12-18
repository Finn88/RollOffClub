import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef, inject, signal } from '@angular/core';
import { RegisterService } from '../../_services/register/register.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../../_services/auth/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  protected subscription: Subscription | null = null;
  protected data = signal<string>('init value');
  private orgService = inject(RegisterService);
  private authService = inject(AuthService);


   ngOnInit() {
    this.subscription = this.orgService.getData()
      .subscribe({
        next: val => {
          this.data.set(val ?? "nothing");
        }
      });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  logOut(): void {
    this.authService.logout();
  }
}
