import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor
{

  // Método correto de injeção para interceptors
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Obtém o token do localStorage
    const token = localStorage.getItem('token');

    // Clona a requisição e adiciona o header Authorization
    if (token)
    {
      const authReq = request.clone({
      setHeaders:
        {
        Authorization: `Bearer ${ token}`
        }
      });
      return next.handle(authReq);
    }

    // Se não houver token, passa a requisição original
    return next.handle(request);
  }
}
