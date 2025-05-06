import { provideRouter, Route, withEnabledBlockingInitialNavigation, withInMemoryScrolling } from '@angular/router';
import { DeviceListComponent } from './components/device-list/device-list.component';
import { PublicLayoutComponent } from './layouts/public-layout/public-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

export const routes: Route[] = [
  {
    path: '', component: PublicLayoutComponent,
    children: [
      { path: '', component: DeviceListComponent },
      //{ path: 'cad-usuario', component: CadUserClientComponent },
      //{ path: 'login', component: LoginComponent },

      //{ path: 'reservar', component: ReservaFormComponent },
      //{ path: 'reserva-cliente', component: ReservaClienteComponent }
      // outras rotas públicas
    ]
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    //canActivate: [AuthGuard, AdminGuard], // Protege toda a rota /admin
    children: [
      //{ path: '', component: AdminDashboardComponent },
      //{ path: 'cad-acomodacao', component: CadAcomodacaoComponent },
      //{ path: 'acomodacoes/editar', component: CadAcomodacaoComponent },
      //{ path: 'reserva-admin', component: ReservaAdminComponent },
      // outras rotas administrativas
    ]
  },   

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
