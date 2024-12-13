import { Component, OnDestroy, OnInit, inject, signal } from '@angular/core';
import { RegisterService } from '../../_services/register/register.service';
import { Subscription } from 'rxjs';
import { TokenStorageService } from '../../_services/token-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  protected subscription: Subscription | null = null;
  protected data = signal<string>('init value');
  private orgService = inject(RegisterService);
  private tockenService = inject(TokenStorageService);
  private router = inject(Router);

  ngOnInit(): void {
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
    this.tockenService.removeToken();
    this.router.navigate(['']);
  }
}
