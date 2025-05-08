import { provideRouter, Route, withEnabledBlockingInitialNavigation, withInMemoryScrolling } from '@angular/router';
import { DeviceListComponent } from './components/device-list/device-list.component';
import { DeviceDetailsComponent } from './components/device-details/device-details.component';
import { PublicLayoutComponent } from './layouts/public-layout/public-layout.component';
import { CommandExecutionComponent } from './components/command-execution/command-execution.component'; 
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth/auth.guard';

export const routes: Route[] = [
  {
    path: '', component: PublicLayoutComponent,
    children: [
      // Rota login
      { path: 'login', component: LoginComponent },
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      // Rota para devices e detalhes de device
      {
        path: 'devices', component: DeviceListComponent, canActivate: [AuthGuard]},
      { path: 'device-details/:id', component: DeviceDetailsComponent, canActivate: [AuthGuard] },
      // Rota para execução de comandos
      {
        path: 'device-details/:id/execute-command/:commandId',
        component: CommandExecutionComponent,
        canActivate: [AuthGuard]
      },
    ]
  }

];

// Configuração do provedor de rotas
export const APP_ROUTER_PROVIDERS = [
  provideRouter(
    routes,
    withInMemoryScrolling({
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled'
    }),
    withEnabledBlockingInitialNavigation() // Opcional, para melhor SSR
  )
];
