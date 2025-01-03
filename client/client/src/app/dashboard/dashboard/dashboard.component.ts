import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef, inject, signal } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { RegisterService } from '../../_services/register/register.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../_services/auth/auth.service';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  imports: [CommonModule, NgFor]
})
export class DashboardComponent implements OnInit, OnDestroy {
  protected subscription: Subscription | null = null;
  protected data = signal<any[]>([]);
  private orgService = inject(RegisterService);
  private authService = inject(AuthService);

   ngOnInit() {
    this.subscription = this.orgService.get()
      .subscribe({
        next: val => {
          this.data.set(val ?? []);
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
