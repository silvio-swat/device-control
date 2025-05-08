import { NgModule } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { NotificationService } from '../services/notification.service';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { InputTextModule } from 'primeng/inputtext';
import { TagModule } from 'primeng/tag';
import { DividerModule } from 'primeng/divider';


@NgModule({
  imports: [
    ToastModule,
    CommonModule,
    TableModule,
    CardModule,
    ButtonModule,
    FormsModule,
    MessageModule,
    InputTextModule,
    TagModule,
    DividerModule

  ],
  exports: [
    ToastModule,
    CommonModule,
    TableModule,
    CardModule,
    ButtonModule,
    FormsModule,
    MessageModule,
    InputTextModule,
    TagModule,
    DividerModule
  ],
  providers: [MessageService, NotificationService]
})
export class SharedStandaloneModule { }
