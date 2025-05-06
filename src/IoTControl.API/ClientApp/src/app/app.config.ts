import { importProvidersFrom, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api'; // Adição única necessária
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Importe FormsModule e ReactiveFormsModule
import { SharedStandaloneModule } from './shared/shared-standalone.module';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura
      }
    }),
    importProvidersFrom(
      BrowserAnimationsModule,
      FormsModule,
      ReactiveFormsModule,
      MessageService,
      SharedStandaloneModule

    ), // animações :contentReference[oaicite:3]{index=3}
    importProvidersFrom(ButtonModule)             // p-button :contentReference[oaicite:4]{index=4}
  ]
};
