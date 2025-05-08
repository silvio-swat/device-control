import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';
import { AuthService } from '../../auth/auth.service'; 

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedStandaloneModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  credentials = { client_id: '', client_secret: '' };
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router) { }

  onSubmit() {
    this.authService.login(
      this.credentials.client_id,
      this.credentials.client_secret
    ).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.access_token);
        this.router.navigate(['/devices']);
      },
      error: (err) => {
        this.errorMessage = 'Falha na autenticação. Verifique as credenciais.';
      }
    });
  }
}





