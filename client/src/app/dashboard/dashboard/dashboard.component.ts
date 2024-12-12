import { Component, OnDestroy, OnInit, inject, signal } from '@angular/core';
import { RegisterService } from '../../_services/register/register.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  protected subscription: Subscription | null = null;
  protected data = signal<string>('init value');
  private orgService = inject(RegisterService);

  ngOnInit(): void {
    this.subscription = this.orgService.getData()
      .subscribe({
        next: val => {
          console.log(val);
          this.data.set(val ?? "nothing");
        }
      });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
