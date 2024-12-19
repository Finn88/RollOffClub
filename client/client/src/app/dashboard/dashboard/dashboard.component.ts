import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef, inject, signal } from '@angular/core';
import { RegisterService } from '../../_services/register/register.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../_services/auth/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  protected subscription: Subscription | null = null;
  protected data = signal<any[]>([]);
  private orgService = inject(RegisterService);
  private authService = inject(AuthService);

  @ViewChild('header', { read: ViewContainerRef })
  viewContainer!: ViewContainerRef;

   async ngOnInit() {
    this.subscription = this.orgService.get()
      .subscribe({
        next: val => {
          this.data.set(val ?? []);
        }
      });

     const module = await import('header/Component');
     const ref = this.viewContainer.createComponent(module.HeaderComponent);
     // const compInstance = ref.instance;
     // compInstance.ngOnInit()
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  logOut(): void {
    this.authService.logout();
  }
}
