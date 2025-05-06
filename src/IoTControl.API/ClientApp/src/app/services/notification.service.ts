// notification.service.ts
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(private messageService: MessageService) { }

  notify(severity: 'success' | 'error' | 'info', message: string, title?: string) {
    this.messageService.add({
      severity,
      summary: title || severity.charAt(0).toUpperCase() + severity.slice(1),
      detail: message,
      life: severity === 'error' ? 5000 : 3000
    });
  }
}
