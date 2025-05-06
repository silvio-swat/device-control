// libs/shared/ui/src/lib/shared-standalone.module.ts
import { NgModule } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';

import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@NgModule({
  imports: [
    ToastModule,
    CommonModule,
    TableModule,
    CardModule,
    ButtonModule,

  ],
  exports: [
    ToastModule,
    CommonModule,
    TableModule,
    CardModule,
    ButtonModule,

  ],
  providers: [MessageService, NotificationService]
})
export class SharedStandaloneModule { }
