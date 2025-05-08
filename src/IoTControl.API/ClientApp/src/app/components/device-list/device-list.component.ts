import { Component, OnInit } from '@angular/core';
import { CiotdService } from '../../services/ciotd.service';
import { Router } from '@angular/router';
import { SharedStandaloneModule } from '../../shared/shared-standalone.module';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [
    SharedStandaloneModule
  ],
  templateUrl: './device-list.component.html',
  styleUrl: './device-list.component.scss'
})
export class DeviceListComponent implements OnInit {
  devices: string[] = [];

  constructor(
    private ciotdService: CiotdService,
    private router: Router,
    private apiService: ApiService
  ) { }

  ngOnInit() {
    this.loadDevices();
  }

  private loadDevices() {
    this.apiService.getDevices().subscribe({
      next: (data) => this.devices = data,
      error: () => alert('Erro ao carregar dispositivos')
    });
  }

  viewDetails(deviceId: string) {
    this.router.navigate(['/device-details', deviceId]);
  } 
}
