import { Component, inject } from '@angular/core';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { AuthService } from '../../auth/auth.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-public-layout',
  imports: [RouterModule, ButtonModule, CommonModule, ToastModule], 
  templateUrl: './public-layout.component.html',
  styleUrl: './public-layout.component.scss'
})
export class PublicLayoutComponent {
  title = 'frontend';
  menuAberto = false;
  private notify = inject(NotificationService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  checkLogin = true;
  private authService = inject(AuthService);
  public isLoggedIn$ = this.authService.isAuthenticated$;

  logout() {
    this.authService.logout();
    this.checkLogin = false;
    this.router.navigate(['/login']);
  }
}
