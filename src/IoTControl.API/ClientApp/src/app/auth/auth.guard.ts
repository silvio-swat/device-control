import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(): boolean {
    // Verifica se existe token no localStorage
    if (!localStorage.getItem('token')) {
      // Redireciona para login se não autenticado
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
